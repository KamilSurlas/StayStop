using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StayStop.BLL.Authorization;
using StayStop.BLL.Dtos.Hotel;
using StayStop.BLL.Dtos.Hotel.HotelOpinion;
using StayStop.BLL.Dtos.Room;
using StayStop.BLL.Dtos.User;
using StayStop.BLL.Exceptions;
using StayStop.BLL.IService;
using StayStop.BLL.Pagination;
using StayStop.BLL_EF.Exceptions;
using StayStop.DAL.Context;
using StayStop.Model;
using System.Linq.Expressions;

namespace StayStop.BLL_EF.Service
{
    public class HotelService : IHotelService
    {
        private readonly StayStopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;


        public HotelService(StayStopDbContext context, IMapper mapper,
            IUserContextService userContextService, IAuthorizationService authorizationService,
            IImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }
        private bool CanHotelBeDeleted(Hotel hotel)
        {
            return !_context.ReservationPositions.Any(rp => rp.Room.Hotel.HotelId == hotel.HotelId);
        }
        private Hotel GetHotelById(int hotelId)
        {
            var hotel = _context.Hotels.
                Include(h => h.Rooms).
                Include(h => h.Managers)
                .Include(h => h.Owner).
                FirstOrDefault(h => h.HotelId == hotelId);

            if (hotel is null) throw new ContentNotFoundException($"Hotel with id: {hotelId} was not found");

            return hotel;
        }
        private User GetUserByMail(string email)
        {
            var user = _context.Users.Include(u => u.ManagedHotels).FirstOrDefault(u => u.Email.Equals(email));
            if (user is null) throw new ContentNotFoundException($"User with email: {email} was not found");
            return user;
        }
        private (double avgOpinion, int count) CalculateAvgOpinions(Hotel hotel)
        {
            var rooms = hotel.Rooms.ToList();

            var roomsId = rooms.Select(r => r.RoomId).ToList();

            var reservationIds = _context.ReservationPositions
                .Where(rp => roomsId.Contains(rp.RoomId))
                .Select(rp => rp.ReservationId)
                .Distinct()
                .ToList();

            var opinions = _context.Opinions
                .Where(o => reservationIds.Contains(o.ReservationId));

            var averageMark = opinions.Average(o => o.Mark);

            var count = opinions.Count();

            return (averageMark, count);
        }

        public int Create(HotelRequestDto hotelDto)
        {
            var hotel = _mapper.Map<Hotel>(hotelDto);
            hotel.OwnerId = _userContextService.GetUserId;

            _context.Add(hotel);
            _context.SaveChanges();

            return hotel.HotelId;
        }

        public void Delete(int hotelId)
        {
            var hotelToDelete = GetHotelById(hotelId);
            if (!CanHotelBeDeleted(hotelToDelete)) throw new InvalidDataException("Hotel can't be deleted - it is assigned to reservations");
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, hotelToDelete, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Permission denied");
            }
            _context.Remove(hotelToDelete);
            _context.SaveChanges();
        }

        public PageResult<HotelResponseDto> GetAll(HotelPagination pagination)
        {
            var baseQuery = _context
            .Hotels
            .Include(h => h.Rooms)
            .Include(h => h.Managers)
            .Where(h => pagination.SearchPhrase == null || (h.Name.ToLower().Contains(pagination.SearchPhrase.ToLower())
            || h.Description.ToLower().Contains(pagination.SearchPhrase.ToLower())
            || h.EmailAddress.ToLower().Contains(pagination.SearchPhrase.ToLower())
            || h.PhoneNumber.ToLower().Contains(pagination.SearchPhrase.ToLower())
            || h.Country.ToLower().Contains(pagination.SearchPhrase.ToLower())
            || h.City.ToLower().Contains(pagination.SearchPhrase.ToLower()))).ToList();

            if (pagination.HotelsSortBy?.Contains("Rating") ?? false)
            {
                Dictionary<Hotel, double> hotelAvgOpinions = new Dictionary<Hotel, double>();
                foreach (var hotel in baseQuery)
                {
                    double avg = CalculateAvgOpinions(hotel).avgOpinion;
                    hotelAvgOpinions.Add(hotel, avg);
                }

                var sortedHotels = pagination.SortDirection == SortDirection.ASC
                    ? hotelAvgOpinions.OrderBy(avgOpinion => avgOpinion.Value)
                    : hotelAvgOpinions.OrderByDescending(avgOpinion => avgOpinion.Value);

                baseQuery = sortedHotels.Select(h => h.Key).ToList();
            }
            else if (!string.IsNullOrEmpty(pagination.HotelsSortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Hotel, object>>>()
                {
                    {nameof(Hotel.Name), h => h.Name },
                    {nameof(Hotel.Stars), h => h.Stars },
                    {nameof(Hotel.Country), h => h.Country },
                    {nameof(Hotel.City), h => h.City },
                };

                var selected = columnsSelector[pagination.HotelsSortBy];

                baseQuery = pagination.SortDirection == SortDirection.ASC
                ? baseQuery.AsQueryable().OrderBy(selected).ToList()
                : baseQuery.AsQueryable().OrderByDescending(selected).ToList();
            }

            if (pagination.Stars is not null)
            {
                baseQuery = baseQuery.Where(h => h.Stars == pagination.Stars).ToList();
            }

            var hotels = baseQuery
              .Skip(pagination.PageSize * (pagination.PageNumber - 1))
            .Take(pagination.PageSize)
            .ToList();



            var hotelResults = _mapper.Map<List<HotelResponseDto>>(hotels);

            var result = new PageResult<HotelResponseDto>(hotelResults, baseQuery.Count(),
                pagination.PageSize, pagination.PageNumber);

            return result;
        }

        public HotelResponseDto GetById(int hotelId)
        {
            var hotel = GetHotelById(hotelId);

            var result = _mapper.Map<HotelResponseDto>(hotel);

            return result;
        }

        public void Update(int hotelId, HotelUpdateRequestDto hotelDto)
        {
            var hotelToUpdate = GetHotelById(hotelId);
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, hotelToUpdate, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Permission denied");
            }



            if (hotelDto.CoverImage is not null && hotelToUpdate.CoverImage is not null)
            {
                hotelToUpdate.Images.Add(hotelToUpdate.CoverImage);

            }

            if (hotelDto.Images?.Any() ?? false)
            {
                foreach (var image in hotelDto.Images)
                {
                    if (!hotelToUpdate.Images.Contains(image))
                    {
                        hotelToUpdate.Images.Add(image);
                    }
                }

            }


            _mapper.Map(hotelDto, hotelToUpdate);
            _context.SaveChanges();
        }

        public void AddManager(int hotelId, string managerEmail)
        {
            var hotel = GetHotelById(hotelId);
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, hotel, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Permission denied");
            }
            var manager = GetUserByMail(managerEmail);

            if (manager.RoleId == 3)
            {
                manager.RoleId = 2;
            }

            hotel.Managers.Add(manager);


            _context.SaveChanges();
        }

        public void RemoveManager(int hotelId, int userId)
        {
            var hotel = GetHotelById(hotelId);
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, hotel, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Permission denied");
            }
            var manager = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (manager is null) throw new ContentNotFoundException($"User with id: {userId} was not found");

            if (!manager.ManagedHotels.Contains(hotel)) throw new InvalidManagerToRemove($"Manager with an id {userId} is not managing hotel with an id {hotelId}");

            hotel.Managers.Remove(manager);

            manager.ManagedHotels.Remove(hotel);

            _context.SaveChanges();
        }

        public HotelOpinionResponseDto GetOpinion(int hotelId)
        {
            var hotel = GetHotelById(hotelId);
            var result = CalculateAvgOpinions(hotel);
            var avg = result.avgOpinion;
            var numberOfOpinions = result.count;

            var response = new HotelOpinionResponseDto()
            {
                AvgOpinion = avg,
                NumberOfOpinions = numberOfOpinions
            };

            return response;
        }

        public List<UserResponseDto> GetManagers(int hotelId)
        {
            var hotel = GetHotelById(hotelId);

            if (hotel.Managers.Count == 0)
            {
                throw new InvalidDataException($"Provided hotel with id: {hotelId} does not have mangaers");
            }

            var managers = _mapper.Map<List<UserResponseDto>>(hotel.Managers);

            return managers;
        }

        public void DeleteImage(int hotelId, string path)
        {
            var hotel = GetHotelById(hotelId);
            if (path is not null && hotel.Images.Contains(path))
            {
                hotel.Images.Remove(path);
            }

            _context.SaveChanges();
        }

        public List<HotelResponseDto> GetAvailable()
        {
            return null;
        }

        public PageResult<HotelResponseDto>? GetAvailable(HotelPagination pagination, DateTime from, DateTime to)
        {
            var results = this.GetAll(pagination);

            var reservations = _context.Reservations
                .Include(r => r.ReservationPositions)
                .ToList();

            var availableHotels = results.Items.Select(hotel =>
            {
                var availableRooms = hotel.Rooms.Where(room =>
                    !reservations.Any(reservation =>
                        reservation.ReservationPositions.Any(rp =>
                            rp.RoomId == room.RoomId &&
                            (from >= reservation.EndDate && to <= reservation.StartDate)
                        )
                    )
                ).ToList();

                var availableRoomDtos = _mapper.Map<List<RoomResponseDto>>(availableRooms);


                var hotelDto = _mapper.Map<HotelResponseDto>(hotel);
                hotelDto.Rooms = availableRoomDtos;

                return hotelDto;
                
            })
            .Where(hotel => hotel.Rooms.Count > 0)
            .ToList();


            results.Items = availableHotels;
            results.TotalItemsCount = availableHotels.Count();

            return results ?? null;
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StayStop.BLL.Dtos.Hotel;
using StayStop.BLL.IService;
using StayStop.BLL.Pagination;
using StayStop.BLL_EF.Exceptions;
using StayStop.DAL.Context;
using StayStop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StayStop.BLL_EF.Service
{
    public class HotelService : IHotelService
    {
        private readonly StayStopDbContext _context;
        private readonly IMapper _mapper;

        public HotelService(StayStopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        private Hotel GetHotelById(int hotelId)
        {
            var hotel = _context.Hotels.
                Include(h => h.Rooms).
                Include(h => h.Managers).
                FirstOrDefault(h => h.HotelId == hotelId);

            if (hotel is null) throw new ContentNotFoundException($"Hotel with id: {hotelId} was not found");

            return hotel;
        }
        private User GetUserByMail(string email)
        {
            var user = _context.Users.Include(u=>u.ManagedHotels).FirstOrDefault(u => u.Email.Equals(email));
            if (user is null) throw new ContentNotFoundException($"User with email: {email} was not found");
            return user;
        }
        private double CalculateAvgOpinions(Hotel hotel)
        {
            var averageRating = hotel.Rooms
            .Where(r=>r.ReservationPositions is not null)
            .SelectMany(r => r.ReservationPositions)
            .Where(rp => rp.Reservation.Opinion is not null)
            .Select(rp => rp.Reservation.Opinion.Mark)
            .DefaultIfEmpty()
            .Average(); 

            return averageRating;
        }
        public int Create(HotelRequestDto hotelDto)
        {
            var hotel = _mapper.Map<Hotel>(hotelDto);

            _context.Add(hotel);
            _context.SaveChanges();

            return hotel.HotelId;
        }

        public void Delete(int hotelId)
        {
            var hotelToDelete = GetHotelById(hotelId);

            _context.Remove(hotelToDelete);
            _context.SaveChanges();
        }

        public PageResult<HotelResponseDto> GetAll(HotelPagination pagination)
        {
            var baseQuery = _context
            .Hotels
            .Include(h => h.Rooms)
            .Include(h => h.Managers)
            .Where(h => pagination.HotelsSortBy == null || (h.Name.ToLower().Contains(pagination.HotelsSortBy.ToLower())
            || h.Stars.ToString().Contains(pagination.HotelsSortBy)
            || h.Country.ToLower().Contains(pagination.HotelsSortBy.ToLower())
            || h.City.ToLower().Contains(pagination.HotelsSortBy.ToLower())));

            if (pagination.HotelsSortBy?.ToLower().Contains("average") ?? false)
            {
                baseQuery = pagination.SortDirection == SortDirection.ASC ? baseQuery.OrderBy(h=>CalculateAvgOpinions(h))
                : baseQuery.OrderByDescending(h => CalculateAvgOpinions(h));
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

                baseQuery = pagination.SortDirection == SortDirection.ASC ? baseQuery.OrderBy(selected)
                : baseQuery.OrderByDescending(selected);
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
            var images = hotelToUpdate.Images;
            if (hotelDto.Images?.Any() == false)
            {        
               images.AddRange(hotelDto.Images);
            }
            hotelDto.Images = images;
            _mapper.Map(hotelDto, hotelToUpdate);
            _context.SaveChanges();
        }

        public void AddManager(int hotelId, string managerEmail)
        {
            var hotel = GetHotelById(hotelId);

            var manager = GetUserByMail(managerEmail);

            if (manager.UserRole == UserRole.User)
            {
                manager.UserRole = UserRole.Manager;
            }

            hotel.Managers.Add(manager);

            manager.ManagedHotels.Add(hotel);

            _context.SaveChanges();
        }

        public void RemoveManager(int hotelId, int userId)
        {
            var hotel = GetHotelById(hotelId);

            var manager = _context.Users.FirstOrDefault(u=>u.UserId == userId);
            if (manager is null) throw new ContentNotFoundException($"User with id: {userId} was not found");

            if (!manager.ManagedHotels.Contains(hotel)) throw new InvalidManagerToRemove($"Manager with an id {userId} is not managing hotel with an id {hotelId}");

            hotel.Managers.Remove(manager);

            manager.ManagedHotels.Remove(hotel); 
            
            _context.SaveChanges();
        }
    }
}

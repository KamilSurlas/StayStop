using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StayStop.BLL.Authorization;
using StayStop.BLL.Dtos.Room;
using StayStop.BLL.Exceptions;
using StayStop.BLL.IService;
using StayStop.BLL_EF.Exceptions;
using StayStop.BLL_EF.Service;
using StayStop.DAL.Context;
using StayStop.Model;

namespace StayStop.BLL_EF.Service
{
    public class RoomService : IRoomService
    {
        private readonly StayStopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IImageService _imageService;

        public RoomService(StayStopDbContext context, IMapper mapper, 
            IUserContextService userContextService, IAuthorizationService authorizationService, 
            IImageService imageService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
            _imageService = imageService;
        }

        private Hotel GetHotelById(int hotelId)
        {
            var hotel = _context.Hotels.
                Include(h => h.Rooms).
                FirstOrDefault(h => h.HotelId == hotelId);

            if (hotel is null) throw new ContentNotFoundException($"Hotel with id: {hotelId} was not found");

            return hotel;
        }
        private Room GetRoomById(int roomId)
        {
            var room = _context.Rooms.
                Include(r => r.ReservationPositions).
                FirstOrDefault(r => r.RoomId == roomId);

            if (room is null) throw new ContentNotFoundException($"Room with id: {roomId} was not found");

            return room;
        }
        public int Create(int hotelId, RoomRequestDto roomDto)
        {         
            var hotel = GetHotelById(hotelId);
            
            var room = _mapper.Map<Room>(roomDto);
            room.HotelId = hotelId;
            room.Hotel = hotel;
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, room , new ResourceOperationRequirement(ResourceOperation.Create)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Permission denied");
            }
            room.HotelId = hotelId;
            room.Hotel = hotel;

            _context.Add(room);
            _context.SaveChanges();

            return room.RoomId;
        }

        public void DeleteAll(int hotelId)
        {
            var hotel = GetHotelById(hotelId);
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, hotel, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Permission denied");
            }
            _context.RemoveRange(hotel.Rooms);

            _context.SaveChanges();       
        }

        public void DeleteById(int hotelId, int roomId)
        {
            var hotel = GetHotelById(hotelId);
            var room = GetRoomById(roomId);
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, room, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Permission denied");
            }
            if (room.HotelId != hotelId) throw new ContentNotFoundException($"Provided hotel id is wrong (hotel id: {hotelId})");
            
            _context.Remove(room);
            _context.SaveChanges();
        }

        public IEnumerable<RoomResponseDto> GetAll(int hotelId)
        {
            var hotel = GetHotelById(hotelId);

            var rooms = _mapper.Map<List<RoomResponseDto>>(hotel.Rooms);
            return rooms;
        }

        public RoomResponseDto GetById(int hotelId, int roomId)
        {
            var hotel = GetHotelById(hotelId);
            var room = GetRoomById(roomId);
          
            if (room.HotelId != hotelId) throw new ContentNotFoundException($"Provided hotel id is wrong (hotel id: {hotelId})");

            var roomResult = _mapper.Map<RoomResponseDto>(room);

            return roomResult;
        }

        public void SetRoomActivity(int hotelId, int roomId)
        {
            var hotel = GetHotelById(hotelId);
            var room = GetRoomById(roomId);
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, room, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Permission denied");
            }
            if (room.HotelId != hotelId) throw new ContentNotFoundException($"Provided hotel id is wrong (hotel id: {hotelId})");

            if (room.IsAvailable) throw new RoomIsAlreadyActive($"Room with {roomId} in hotel {hotelId} is already active");

            room.IsAvailable = true;
            _context.SaveChanges();
        }

        public void Update(int hotelId, int roomId, RoomUpdateRequestDto roomDto)
        {
            var hotel = GetHotelById(hotelId);
            var room = GetRoomById(roomId);

            if (room.HotelId != hotelId)
                throw new ContentNotFoundException($"Provided hotel id is wrong (hotel id: {hotelId})");

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, room, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Permission denied");
            }
            var roomsFromDb = room.Images.ToList();
            if (roomDto.Images is not null && roomDto.Images.Any())
            {
                roomsFromDb.AddRange(roomDto.Images);
            }

            roomDto.Images = roomsFromDb;
            _mapper.Map(roomDto, room);

            room.HotelId = hotelId;

            _context.SaveChanges();
        }

        public void UploadCoverImage(int hotelId, int roomId, IFormFile coverImage)
        {
            if (coverImage != null && coverImage.Length > 0)
            {
                var hotel = GetHotelById(hotelId);
                var room = GetRoomById(roomId);
                if (room.HotelId != hotelId)
                    throw new ContentNotFoundException($"Provided hotel id is wrong (hotel id: {hotelId})");
                var roomOldCoverImage = room.CoverImage;
                if (roomOldCoverImage is not null && !room.Images.Contains(coverImage.FileName))
                {
                    room.Images.Add(coverImage.FileName);
                }
                room.CoverImage = _imageService.UploadImage(coverImage);
                _context.SaveChanges();
            }
        }

      
    }
}


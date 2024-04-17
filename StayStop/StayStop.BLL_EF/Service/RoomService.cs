using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StayStop.BLL.Dtos.Room;
using StayStop.BLL.IService;
using StayStop.BLL_EF.Exceptions;
using StayStop.DAL.Context;
using StayStop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace StayStop.BLL_EF.Service
{
    public class RoomService : IRoomService
    {
        private readonly StayStopDbContext _context;
        private readonly IMapper _mapper;

        public RoomService(StayStopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            if (roomDto.PriceForChild <= 0.0M || roomDto.PriceForAdult <= 0.0M) throw new InvalidDataException("$Price can't be lower or equal to 0");


            var hotel = GetHotelById(hotelId);

            var room = _mapper.Map<Room>(roomDto);

            room.HotelId = hotelId;
            room.Hotel = hotel;

            _context.Add(room);
            _context.SaveChanges();

            return room.RoomId;
        }

        public void DeleteAll(int hotelId)
        {
            var hotel = GetHotelById(hotelId);
            
            _context.RemoveRange(hotel.Rooms);

            _context.SaveChanges();       
        }

        public void DeleteById(int hotelId, int roomId)
        {
            var hotel = GetHotelById(hotelId);
            var room = GetRoomById(roomId);
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
            if (room.HotelId != hotelId) throw new ContentNotFoundException($"Provided hotel id is wrong (hotel id: {hotelId})");

            if (room.IsAvailable) throw new RoomIsAlreadyActive($"Room with {roomId} in hotel {hotelId} is already active");
        }

        public void Update(int hotelId, int roomId, RoomUpdateRequestDto roomDto)
        {
            if (roomDto.PriceForChild <= 0.0M || roomDto.PriceForAdult <= 0.0M) 
                throw new InvalidDataException("$Price can't be lower or equal to 0");

            var hotel = GetHotelById(hotelId);
            var room = GetRoomById(roomId);
            
            if (room.HotelId != hotelId) 
                throw new ContentNotFoundException($"Provided hotel id is wrong (hotel id: {hotelId})");

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
    }
}

using AutoMapper;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using StayStop.BLL.Dtos.ReservationPosition;
using StayStop.BLL.IService;
using StayStop.BLL_EF.Exceptions;
using StayStop.DAL.Context;
using StayStop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL_EF.Service
{
    public class ReservationPositionService : IReservationPositionService
    {
        private readonly StayStopDbContext _context;
        private readonly IMapper _mapper;

        public ReservationPositionService(StayStopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        private Reservation GetReservationById(int reservationId)
        {
            var reservation = _context.Reservations.First(r => r.ReservationId == reservationId);
            if (reservation is null) throw new ContentNotFoundException($"Reservation with id: {reservationId} was not found");

            return reservation;
        }
        private ReservationPosition GetReservationPositionById(int reservationPositionId)
        {
            var reservationPosition = _context.ReservationPositions.First(r => r.ReservationPositionId == reservationPositionId);
            if (reservationPosition is null) throw new ContentNotFoundException($"Reservation position with id: {reservationPositionId} was not found");

            return reservationPosition;
        }
        public int Create(int reservationId, ReservationPositionRequestDto reservationPositionDto)
        {
            var roomInReservationPistion = _context.Rooms.FirstOrDefault(r => r.RoomId == reservationPositionDto.RoomId);

            if (roomInReservationPistion is null) throw new ContentNotFoundException($"Unable to add reservation position! Room with id: {reservationPositionDto.RoomId} was not found");

            if (!roomInReservationPistion.IsAvailable) throw new InvalidDataException("Unable to add reservation position! Make sure to add available room");
  
            var reservation = GetReservationById(reservationId);

            var reservationPosition = _mapper.Map<ReservationPosition>(reservationPositionDto);
            // tutaj mozna sprobowac zrobic to w mapperze - trzeba by bylo jakos pobrac tam z bazy pokoj po id z Dto
            reservationPosition.Price = (roomInReservationPistion.PriceForAdult * reservationPosition.NumberOfAdults + roomInReservationPistion.PriceForChild * reservationPosition.NumberOfChildren) * reservationPosition.Amount;
            reservationPosition.ReservationId = reservationId;
            reservationPosition.Reservation = reservation;

            _context.Add(reservationPosition);
            _context.SaveChanges();

            return reservationPosition.ReservationId;
        }

        public void DeleteReservationPosition(int reservationId, int reservationPositionId)
        {
            var reservationPosition = GetReservationPositionById(reservationPositionId);
            var reservation = GetReservationById(reservationId); 

            if(reservationPosition.ReservationId!=reservationId) throw new ContentNotFoundException($"Provided reservation id is wrong (reservation id: {reservationId})");

            _context.Remove(reservationPosition);
            _context.SaveChanges();
        }

        public ReservationPositionResponseDto GetById(int reservationId, int reservationPositionId)
        {
            var reservationPosition = GetReservationPositionById(reservationPositionId);
            var reservation = GetReservationById(reservationId);

            if (reservationPosition.ReservationId != reservationId) throw new ContentNotFoundException($"Provided reservation id is wrong (reservation id: {reservationId})");

            var result=_mapper.Map<ReservationPositionResponseDto>(reservationPosition);

            return result;
        }

        public IEnumerable<ReservationPositionResponseDto> GetReservationPositions(int reservationId)
        {
            var reservation=GetReservationById(reservationId);

            var results=_mapper.Map<IEnumerable<ReservationPositionResponseDto>>(reservation);

            return results;
        }
    }
}

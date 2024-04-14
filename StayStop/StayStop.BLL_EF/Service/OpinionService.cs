using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StayStop.BLL.Dtos.Opinion;
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
    public class OpinionService : IOpinionService
    {
        private readonly StayStopDbContext _context;
        private readonly IMapper _mapper;

        public OpinionService(StayStopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private Reservation GetReservationById(int reservationId)
        {
            var reservation = _context.Reservations.
                Include(r => r.Opinion).
                FirstOrDefault(r => r.ReservationId == reservationId);

            if (reservation is null) throw new ContentNotFoundException($"Reservation with id: {reservationId} was not found");

            return reservation;
        }
        public int Create(int reservationId, OpinionRequestDto opinionDto)
        {
            if (opinionDto.Mark < 1 || opinionDto.Mark > 5) throw new InvalidDataException($"Mark is invalid: {opinionDto.Mark}");
   

            var reservation = GetReservationById(reservationId);

            if (reservation.Opinion is not null) throw new ReservationAlreadyHasOpinion($"Reservation with an id {reservationId} already has opinion");

            var opinion = _mapper.Map<Opinion>(opinionDto);

            opinion.ReservationId = reservationId;

            reservation.Opinion = opinion;

            _context.Add(opinion);

            _context.SaveChanges();

            return opinion.OpinionId;
        }

        public void Delete(int reservationId)
        {
            var reservation = GetReservationById(reservationId);

            if (reservation.Opinion is null) throw new InvalidOperationException($"Reservation with an id {reservationId} don't have opinion");

            reservation.Opinion = null;

            _context.Opinions.Remove(reservation.Opinion);

            _context.SaveChanges();
        }

        public OpinionResponseDto GetByReservationId(int reservationId)
        {
            var reservation = GetReservationById(reservationId);

            if (reservation.Opinion is null) throw new InvalidOperationException($"Reservation with an id {reservationId} don't have opinion");

            var opinion = _mapper.Map<OpinionResponseDto>(reservation.Opinion);

            return opinion;
        }

        public void Update(int reservationId, OpinionUpdateRequestDto opinionDto)
        {
            if (opinionDto.Mark < 1 || opinionDto.Mark > 5) throw new InvalidDataException($"Mark is invalid: {opinionDto.Mark}");

            var reservation = GetReservationById(reservationId);

            if (reservation.Opinion is null) throw new InvalidOperationException($"Reservation with an id {reservationId} don't have opinion");

            _mapper.Map(reservation.Opinion, opinionDto);

            reservation.Opinion.ReservationId = reservationId;

            _context.SaveChanges();
        }
    }
}

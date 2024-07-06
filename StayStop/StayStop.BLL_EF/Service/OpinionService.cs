using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using StayStop.BLL.Authorization;
using StayStop.BLL.Dtos.Hotel.HotelOpinion;
using StayStop.BLL.Dtos.Opinion;
using StayStop.BLL.Exceptions;
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
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;

        public OpinionService(StayStopDbContext context, IMapper mapper, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
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

            if (reservation.UserId != _userContextService.GetUserId)
            {
                throw new ForbiddenException("Access denied");
            }

            var opinion = _mapper.Map<Opinion>(opinionDto);
            opinion.AddedById = _userContextService.GetUserId;
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
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, reservation.Opinion, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Permission denied");
            }
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

        public void UpdateByOpinionId(int opinionId, OpinionUpdateRequestDto opinionDto)
        {
            if (opinionDto.Mark < 1 || opinionDto.Mark > 5) throw new InvalidDataException($"Mark is invalid: {opinionDto.Mark}");
            var opinion = _context.Opinions
              .Include(o => o.Reservation)
              .ThenInclude(r => r.ReservationPositions)
              .ThenInclude(rp => rp.Room).Where(o => o.OpinionId == opinionId).FirstOrDefault();
            if (opinion is null)
            {
                throw new ContentNotFoundException($"Opinion with id: {opinionId} was not found");
            }


            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, opinion, new ResourceOperationRequirement(ResourceOperation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Permission denied");
            }
            _mapper.Map(opinionDto, opinion);

         

            _context.SaveChanges();
        }

        public List<OpinionResponseDto> GetUserOpinions()
        {
            var opinions = _context.Opinions.Include(o => o.Reservation)
            .ThenInclude(r => r.ReservationPositions)
                .ThenInclude(rp => rp.Room)
                    .ThenInclude(room => room.Hotel).Where(o => o.AddedById == _userContextService.GetUserId).ToList();

            var results = _mapper.Map<List<OpinionResponseDto>>(opinions);

            return results;
        }

        public void DeleteByOpinionId(int opinionId)
        {
            var opinion = _context.Opinions
                .FirstOrDefault(o => o.OpinionId == opinionId);

            if (opinion is null)
            {
                throw new ContentNotFoundException($"Opinion with id: {opinionId} was not found");

               
            }
            _context.Remove(opinion);
            _context.SaveChanges();
        }

        public OpinionResponseDto GetByOpinionId(int opinionId)
        {
            var opinion = _context.Opinions
                .Include(o => o.Reservation)
                .ThenInclude(r => r.ReservationPositions)
                .ThenInclude(rp => rp.Room)
                    .ThenInclude(room => room.Hotel).Where(o => o.OpinionId == opinionId).FirstOrDefault();

            if (opinion is null)
            {
                throw new ContentNotFoundException($"Opinion with id: {opinionId} was not found");
            }

            var result = _mapper.Map<OpinionResponseDto>(opinion);
            return result;
        }
    }
}

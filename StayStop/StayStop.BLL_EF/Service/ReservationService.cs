using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StayStop.BLL.Dtos.Reservation;
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
    public class ReservationService : IReservationService
    {
        private readonly StayStopDbContext _context;
        private readonly IMapper _mapper;

        public ReservationService(StayStopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        private User GetUserById(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user is null) throw new ContentNotFoundException($"User with id: {userId} was not found");

            return user;
        }
        public int Create(ReservationRequestDto reservationDto)
        {
            if (reservationDto.StartDate <= reservationDto.EndDate) throw new InvalidReservationDate($"Reservation end date can't equal or before start date");
 
            //tutaj id uzytkownika będzie pobierane z tokenu JWT - narazie 
            //ustawiliśmy na sztywno w celu pokazania na sprawozdaniu.

            var user = GetUserById(0); // id bedzie pobierane z tokenu JWT

            var reservation = _mapper.Map<Reservation>(reservationDto);

            reservation.User = user;
            reservation.UserId = user.UserId;

            user.UserReservations.Add(reservation);

            _context.Add(reservation);

            _context.SaveChanges();

            return reservation.ReservationId;
            
        }

        public void DeleteById(int reservationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservationResponseDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public ReservationResponseDto GetUserReservationById(int userId, int reservationId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReservationResponseDto> GetUserReservations(int userId)
        {
            throw new NotImplementedException();
        }
    }
}

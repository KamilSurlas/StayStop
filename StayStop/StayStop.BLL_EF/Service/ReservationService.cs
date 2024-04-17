using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StayStop.BLL.Dtos.Hotel;
using StayStop.BLL.Dtos.Reservation;
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
            var user = _context.Users.Include(u=>u.UserReservations).FirstOrDefault(u => u.UserId == userId);
            if (user is null) throw new ContentNotFoundException($"User with id: {userId} was not found");

            return user;
        }
        private Reservation GetReservationById(int reservationId)
        {
            var reservation = _context.Reservations.First(r => r.ReservationId == reservationId);
            if (reservation is null) throw new ContentNotFoundException($"Reservation with id: {reservationId} was not found");

            return reservation;
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
            var reservation = GetReservationById(reservationId);
            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }

        public PageResult<ReservationResponseDto> GetAll(ReservationPagination pagination)
        {
            var baseQuery = _context
           .Reservations
           .Include(r => r.User)
           .Include(r => r.ReservationPositions).AsQueryable();
  
        
           
            if (!string.IsNullOrEmpty(pagination.ReservationsSortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Reservation, object>>>()
                {
                    {nameof(Reservation.Price), r => r.Price },                 
                    {nameof(Reservation.StartDate), r => r.StartDate},                 
                    {nameof(Reservation.EndDate), r => r.EndDate },                 
                };

                var selected = columnsSelector[pagination.ReservationsSortBy];

                baseQuery = pagination.SortDirection == SortDirection.ASC ? baseQuery.OrderBy(selected)
                : baseQuery.OrderByDescending(selected);
            }


            var reservations = baseQuery
              .Skip(pagination.PageSize * (pagination.PageNumber - 1))
            .Take(pagination.PageSize)
            .ToList();

            var reservationsResults = _mapper.Map<List<ReservationResponseDto>>(reservations);

            var result = new PageResult<ReservationResponseDto>(reservationsResults, baseQuery.Count(), pagination.PageSize, pagination.PageNumber);

            return result;
        }

        public ReservationResponseDto GetUserReservationById(int userId, int reservationId)
        {
            var user = GetUserById(userId);
            var reservation = GetReservationById(reservationId);
            if (reservation.UserId!=userId) throw new ContentNotFoundException($"Provided reservation id is wrong (reservation id: {reservationId})");

            var result = _mapper.Map<ReservationResponseDto>(reservation);

            return result;
        }

        public IEnumerable<ReservationResponseDto> GetUserReservations(int userId)
        {
            var user = GetUserById(userId);

            if (user.UserReservations?.Count == 0) throw new ContentNotFoundException($"User with id: {userId} don't have reservation history");

            var results = _mapper.Map<IEnumerable<ReservationResponseDto>>(user.UserReservations);

            return results;

        }
    }
}

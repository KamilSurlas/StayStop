using StayStop.BLL.Dtos.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.IService
{
    public interface IReservationService
    {
        int Create();
        ReservationResponseDto GetUserReservationById(int userId, int reservationId);
        IEnumerable<ReservationResponseDto> GetUserReservations(int userId);
        IEnumerable<ReservationResponseDto> GetAll();
        void DeleteById(int userId, int reservationId);
    }
}

using StayStop.BLL.Dtos.ReservationPosition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.IService
{
    public interface IReservationPositionService
    {
        int Create(ReservationPositionRequestDto reservationPositionDto);
        ReservationPositionResponseDto GetById(int userId, int reservationPositionId);
        IEnumerable<ReservationPositionRequestDto> GetUserReservationPositions(int userId);
        IEnumerable<ReservationPositionRequestDto> GetAll();
    }
}

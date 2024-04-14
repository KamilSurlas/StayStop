using StayStop.BLL.Dtos.ReservationPosition;
using StayStop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.IService
{
    public interface IReservationPositionService
    {
        int Create(int reservationId, ReservationPositionRequestDto reservationPositionDto);
        ReservationPositionResponseDto GetById(int reservationId, int reservationPositionId);
        IEnumerable<ReservationPositionResponseDto> GetReservationPositions(int reservationId);
        void DeleteReservationPosition(int reservationId, int reservationPositionId);
    }
}

using StayStop.BLL.Dtos.ReservationPosition;
using StayStop.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.Dtos.Reservation
{
    public class ReservationRequestDto
    {
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required List<ReservationPositionRequestDto> ReservationPositions { get; set; }
        public required ReservationStatus ReservationStatus { get; set; } = ReservationStatus.Booked;

    }
}

using StayStop.BLL.Dtos.ReservationPosition;
using StayStop.Model;
using StayStop.Model.Enums;

namespace StayStop.BLL.Dtos.Reservation
{
    public class ReservationResponseDto
    {
        public required int ReservationId { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required decimal Price { get; set; }
        public required List<ReservationPositionResponseDto> ReservationPositions { get; set; }
        public required ReservationStatus ReservationStatus { get; set; }
    }
}


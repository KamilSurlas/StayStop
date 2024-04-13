using StayStop.BLL.Dtos.ReservationPosition;
using StayStop.Model;

namespace StayStop.BLL.Dtos.Reservation
{
    public class ReservationResponseDto
    {
        public required DateTime Date { get; set; }
        public required decimal Price { get; set; }
        public required List<ReservationPositionResponseDto> ReservationPositions { get; set; }
    }
}


namespace StayStop.BLL.Dtos.Reservation.Helpers.Models
{
    public class ReservationDetailsDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int NumOfAdults { get; set; }
        public int NumOfChildren { get; set; }
    }
}

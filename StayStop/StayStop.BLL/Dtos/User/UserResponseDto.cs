using StayStop.BLL.Dtos.Hotel;
using StayStop.BLL.Dtos.Reservation;
using System.ComponentModel.DataAnnotations;

namespace StayStop.BLL.Dtos.User
{
    public class UserResponseDto
    {
        public required int UserId { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public List<ReservationResponseDto>? UserReservations { get; set; } 
        public List<HotelResponseDto>? ManagedHotels { get; set; } 
        public List<HotelResponseDto>? OwnedHotels { get; set; } 
    }
}
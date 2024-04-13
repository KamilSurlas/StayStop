using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public required string Email { get; set; }
        public required string HashedPassword { get; set; }
        public required string PhoneNumber { get; set; }
        public UserRole UserRole { get; set; } = UserRole.User;
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public List<Reservation>? UserReservations { get; set; }
        public List<ReservationPosition>? UserReservationPositions { get; set; }
        public List<Hotel>? ManagedHotels { get; } = [];
    }
}

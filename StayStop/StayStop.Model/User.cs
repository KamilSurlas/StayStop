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
        [MaxLength(30)]
        public required string Email { get; set; }
        public required string HashedPassword { get; set; }
        [MaxLength(30)]
        public required string PhoneNumber { get; set; }
        public UserRole UserRole { get; set; } = UserRole.User;
        [MaxLength(30)]
        public required string Name { get; set; }
        [MaxLength(30)]
        public required string LastName { get; set; }
        public List<Reservation>? UserReservations { get; set; }
        public List<Hotel>? ManagedHotels { get; set; } = [];
    }
}

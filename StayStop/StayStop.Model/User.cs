using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [MaxLength(30)]
        public required string Name { get; set; }
        [MaxLength(30)]
        public required string LastName { get; set; }
        [ForeignKey(nameof(RoleId))]
        public int RoleId { get; set; } = 3;
        public Role Role { get; set; }
        public List<Reservation>? UserReservations { get; set; }
        public List<Hotel>? ManagedHotels { get; set; } = [];
    }
}

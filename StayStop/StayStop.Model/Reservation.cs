using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StayStop.Model
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public required User User { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; }
        public required decimal Price { get; set; }
        public Opinion? Opinion { get; set; }
        public required List<ReservationPosition> ReservationPositions { get; set; }
    }
}
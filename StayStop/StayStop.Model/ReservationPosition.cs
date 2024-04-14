using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.Model
{
    public class ReservationPosition
    {
        [Key]
        public int ReservationPositionId { get; set; }
        public int ReservationId { get; set; }
        [ForeignKey(nameof(ReservationId))]
        public required Reservation Reservation { get; set; }
        public required int RoomId { get; set; }
        [ForeignKey(nameof(RoomId))]
        public required Room Room { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public required decimal Price { get; set; }
        public required int Amount { get; set; } 
    }
}

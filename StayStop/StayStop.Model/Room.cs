using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.Model
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
        [MaxLength(200)]
        public required string Description { get; set; }
        public required RoomType RoomType { get; set; }
        public required string CoverImage { get; set; }
        public required List<string> Images { get; set; }
        public required bool IsAvailable { get; set; }
        public required decimal PriceForAdult { get; set; }
        public required decimal PriceForChild { get; set; }
        public required int HotelId { get; set; }
        [ForeignKey(nameof(HotelId))]
        public required Hotel Hotel { get; set; }
        public List<ReservationPosition>? ReservationPositions { get; set; }
    }
}

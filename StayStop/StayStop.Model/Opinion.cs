using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StayStop.Model
{
    public class Opinion
    {
        [Key]
        public int OpinionId { get; set; }
        public required int ReservationId { get; set; }

        [ForeignKey(nameof(ReservationId))]
        public required Reservation Reservation { get; set; }
        public required int Mark { get; set; }
        [MaxLength(250)]
        public required string UserOpinion { get; set; }
        public required string Details { get; set; }
        public required int? AddedById { get; set; }
        public required User? AddedBy { get; set; }

    }
}

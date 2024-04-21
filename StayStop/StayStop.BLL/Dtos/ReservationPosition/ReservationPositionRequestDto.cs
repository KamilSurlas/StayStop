using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.Dtos.ReservationPosition
{
    public class ReservationPositionRequestDto
    {
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public required int Amount { get; set; } = 1;
        public required int RoomId { get; set; }
    }
}

using StayStop.BLL.Dtos.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.Dtos.ReservationPosition
{
    public class ReservationPositionResponseDto
    {
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public required decimal Price { get; set; }
        public required int Amount { get; set; }
        public required RoomResponseDto Room { get; set; }
    }
}

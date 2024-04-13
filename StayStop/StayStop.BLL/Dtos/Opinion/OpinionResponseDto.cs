using StayStop.BLL.Dtos.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.Dtos.Opinion
{
    public class OpinionResponseDto
    {
        public required RoomResponseDto Room { get; set; }
        public required int Mark { get; set; }
        public required string UserOpinion { get; set; }
        public required string Details { get; set; }
    }
}

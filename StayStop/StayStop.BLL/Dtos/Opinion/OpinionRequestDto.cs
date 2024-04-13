using StayStop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.Dtos.Opinion
{
    public class OpinionRequestDto
    {
        public required int Mark { get; set; }
        public required string UserOpinion { get; set; }
        public required string Details { get; set; }
    }
}

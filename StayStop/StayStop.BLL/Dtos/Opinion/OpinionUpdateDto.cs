using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.Dtos.Opinion
{
    public class OpinionUpdateDto
    {
        public int? Mark { get; set; }
        public string? UserOpinion { get; set; }
        public string? Details { get; set; }
    }
}

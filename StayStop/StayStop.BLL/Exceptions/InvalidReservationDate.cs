using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL_EF.Exceptions
{
    public class InvalidReservationDate : Exception
    {
        public InvalidReservationDate(string mes):base(mes)
        {
            
        }
    }
}

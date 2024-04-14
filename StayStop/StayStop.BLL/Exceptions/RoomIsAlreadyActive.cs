using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL_EF.Exceptions
{
    public class RoomIsAlreadyActive : Exception
    {
        public RoomIsAlreadyActive(string mes) : base(mes)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string mes):base(mes)
        {
            
        }
    }
}

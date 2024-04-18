using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.Exceptions
{
    public class BadEmailOrPassword:Exception
    {
        public BadEmailOrPassword(string mes):base(mes)
        {
            
        }
    }
}

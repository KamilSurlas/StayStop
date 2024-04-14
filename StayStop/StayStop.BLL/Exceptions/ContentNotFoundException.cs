using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL_EF.Exceptions
{
    public class ContentNotFoundException : Exception
    {
        public ContentNotFoundException(string mes):base(mes)
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.Dtos.User
{
    public class UserLoginDto
    {
        public required string EmailAddress { get; set; }
        public required string Password { get; set; }
    }
}

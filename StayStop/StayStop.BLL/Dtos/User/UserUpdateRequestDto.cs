using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.Dtos.User
{
    public class UserUpdateRequestDto
    {
        public string? EmailAddress { get; set; }
        public string? CurrentPassword { get; set; }
        public string? Password { get; set; }
        public string? ConfirmedPassword { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.Dtos.User
{
    public class UserRegisterDto
    {
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
        [Phone]
        public required string PhoneNumber { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
    }
}

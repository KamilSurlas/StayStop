using AutoMapper;
using StayStop.BLL.Dtos.User;
using StayStop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
namespace StayStop.BLL_EF.Mapper
{
    public class RegisterUserConverter : ITypeConverter<UserRegisterDto, User>
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        public RegisterUserConverter(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public User Convert(UserRegisterDto source, User destination, ResolutionContext context)
        {
            destination = new User()
            {
                Email = source.Email,
                Name = source.Name,
                PhoneNumber = source.PhoneNumber,
                LastName = source.LastName,
                HashedPassword = null!
            };

            destination.HashedPassword = _passwordHasher.HashPassword(destination, source.Password);
            return destination;
        }
    }
}

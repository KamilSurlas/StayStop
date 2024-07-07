using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StayStop.BLL.Dtos.User;
using StayStop.Model;

namespace StayStop.BLL_EF.Mapper
{
    public class UpdateUserConverter : ITypeConverter<UserUpdateRequestDto, User>
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        public UpdateUserConverter(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        public User Convert(UserUpdateRequestDto source, User destination, ResolutionContext context)
        {

            destination.Email = source.EmailAddress ?? destination.Email;
            destination.Name = source.Name ?? destination.Name;
            destination.PhoneNumber = source.PhoneNumber ?? destination.PhoneNumber;
            destination.LastName = source.LastName ?? destination.LastName;
            destination.HashedPassword = _passwordHasher.HashPassword(destination, source.Password);

            return destination;
        }
    }
}

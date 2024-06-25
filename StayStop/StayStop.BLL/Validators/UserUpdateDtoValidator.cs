using FluentValidation;
using StayStop.BLL.Dtos.User;
using StayStop.Model;
using Microsoft.AspNetCore.Identity;
using StayStop.DAL.Context;
using StayStop.BLL.IService;
using System.Linq;


namespace StayStop.BLL.Validators
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateRequestDto>
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly StayStopDbContext _context;
        private readonly IUserContextService _contextService;

        public UserUpdateDtoValidator(IPasswordHasher<User> passwordHasher, StayStopDbContext dbcontext, 
            IUserContextService contextService)
        {
            _passwordHasher = passwordHasher;
            _context = dbcontext;
            _contextService = contextService;

            var id = _contextService.GetUserId;
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);

            
            RuleFor(dto => dto.CurrentPassword).Custom((value, context) => {
                if (value != null)
                {
                    var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, value);

                    if (result != PasswordVerificationResult.Success)
                    {
                        context.AddFailure("CurrentPassword", $"Password is not correct");
                    }
                }
            });
        }

        
    }
}

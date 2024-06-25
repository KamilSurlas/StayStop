using FluentValidation;
using StayStop.BLL.Dtos.User;
using StayStop.DAL.Context;

namespace StayStop.BLL.Validators
{
    public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        private readonly StayStopDbContext _context;
        public UserRegisterDtoValidator(StayStopDbContext context)
        {
            _context = context;

            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
            RuleFor(x => x.ConfirmPassword).Equal(u => u.Password)
                .WithMessage("Passwords aren't the same");
            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var existingEmail = _context.Users.Any(u => u.Email == value);
                if (existingEmail)
                {
                    context.AddFailure("Email", $"Email {value} is already in use");
                }
            });
        }
    }
}
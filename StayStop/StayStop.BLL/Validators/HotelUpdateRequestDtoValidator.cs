using FluentValidation;
using StayStop.BLL.Dtos.Hotel;
using StayStop.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.Validators
{
    public class HotelUpdateRequestDtoValidator : AbstractValidator<HotelUpdateRequestDto>
    {
        private readonly StayStopDbContext _context;

        public HotelUpdateRequestDtoValidator(StayStopDbContext context)
        {
            _context = context;

            RuleFor(x => x.Stars).InclusiveBetween(1, 5);
            RuleFor(x => x.PhoneNumber).MinimumLength(9).MaximumLength(13).Custom((value, context) =>
            {
                var existingPhoneNumber = _context.Users.Any(u => u.PhoneNumber == value);
                if (existingPhoneNumber)
                {
                    context.AddFailure("PhoneNumber", $"Phone number {value} is already in use");
                }
            });
            RuleFor(x => x.EmailAddress).EmailAddress().Custom((value, context) =>
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

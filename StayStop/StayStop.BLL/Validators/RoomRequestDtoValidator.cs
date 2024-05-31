using FluentValidation;
using StayStop.BLL.Dtos.Room;

namespace StayStop.BLL.Validators
{
    public class RoomRequestDtoValidator : AbstractValidator<RoomRequestDto>
    {
        public RoomRequestDtoValidator()
        {
            RuleFor(dto => dto.PriceForAdult).GreaterThanOrEqualTo(0);
            RuleFor(dto => dto.PriceForChild).GreaterThanOrEqualTo(0);
        }
    }
}

using FluentValidation;
using StayStop.BLL.Dtos.Room;

namespace StayStop.BLL.Validators
{
    public class RoomUpdateRequestDtoValidator : AbstractValidator<RoomUpdateRequestDto>
    {
        public RoomUpdateRequestDtoValidator()
        {
            RuleFor(dto => dto.PriceForAdult).GreaterThanOrEqualTo(0);
            RuleFor(dto => dto.PriceForChild).GreaterThanOrEqualTo(0);
        }
    }
}

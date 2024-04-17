using FluentValidation;
using StayStop.BLL.Pagination;
using StayStop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayStop.BLL.Validators
{
    public class ReservationPaginationValidator:AbstractValidator<ReservationPagination>
    {
        private int[] allowedPageSizes = { 5,10, 15, 20 };
        private string[] allowedSortByNames =
        {
            nameof(Reservation.Price),nameof(Reservation.EndDate),nameof(Reservation.StartDate)
        };


        public ReservationPaginationValidator()
        {        
            RuleFor(p => p.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(p => p.PageSize).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(",", allowedPageSizes)}]");
                }
            });
            RuleFor(p => p.ReservationsSortBy).Must(value => string.IsNullOrEmpty(value) || allowedSortByNames.Contains(value))
            .WithMessage($"Sort by is optional or must be in [{string.Join(", ", allowedSortByNames)}]");
        }
    }
}

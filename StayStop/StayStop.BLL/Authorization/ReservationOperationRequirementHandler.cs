using Microsoft.AspNetCore.Authorization;
using StayStop.Model;
using StayStop.Model.Constants;
using System.Security.Claims;

namespace StayStop.BLL.Authorization
{
    public class ReservationOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Reservation>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Reservation reservation)
        {
            var userId = context.User?.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;           
           
            if (requirement.ResourceOperation == ResourceOperation.Update)
            {
                if(reservation.UserId == int.Parse(userId))
                {
                    context.Succeed(requirement);
                }
            }
            if (context.User?.IsInRole(UserRole.Admin) ?? false)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}

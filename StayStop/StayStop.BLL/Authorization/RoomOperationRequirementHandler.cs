using Microsoft.AspNetCore.Authorization;
using StayStop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
namespace StayStop.BLL.Authorization
{
    public class RoomOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Room>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Room room)
        {
            if (requirement.ResourceOperation == ResourceOperation.Read)
            {
                context.Succeed(requirement);
            }
            var userId = context.User?.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (room.Hotel.OwnerId == int.Parse(userId))
            {
                context.Succeed(requirement);
            }
            if (requirement.ResourceOperation == ResourceOperation.Create || requirement.ResourceOperation == ResourceOperation.Update)
            {
                if (room.Hotel.Managers.Any(manager => manager.UserId == int.Parse(userId)))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }
            
            return Task.CompletedTask;
        }
    }
}

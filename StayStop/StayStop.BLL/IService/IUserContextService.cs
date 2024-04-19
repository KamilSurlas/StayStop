using System.Security.Claims;

namespace StayStop.BLL.IService
{
    public interface IUserContextService
    {
        int? GetUserId { get; }
        ClaimsPrincipal? User { get; }
    }
}
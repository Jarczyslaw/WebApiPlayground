using System.Security.Claims;

namespace WebApiPlayground.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? UserId { get; }
    }
}
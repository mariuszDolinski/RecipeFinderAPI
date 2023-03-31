using System.Security.Claims;

namespace RecipeFinderAPI.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        int? GetUserId { get; }
    }

    public class UserContextService : IUserContextService
    {
            private readonly IHttpContextAccessor _contextAccessor;

            public UserContextService(IHttpContextAccessor contextAccessor)
            {
                _contextAccessor = contextAccessor;
            }

            public ClaimsPrincipal User => _contextAccessor.HttpContext?.User;
            public int? GetUserId => User is null ? null
            : int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}

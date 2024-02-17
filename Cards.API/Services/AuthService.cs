using Cards.Infrastructure.DataTypes;
using Cards.Services.DTOModels;
using Cards.Services.Interfaces;

namespace Cards.Services;

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public CurrentUserDTO GetCurrentUser()
    {
        var result = new CurrentUserDTO();
        var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
        var roleName = _httpContextAccessor.HttpContext?.Items["Role"]?.ToString();

        if (Guid.TryParse(userId, out Guid guidId))
        {
            result.Id = guidId;
        }

        if (Enum.TryParse(roleName, out UserRoles role))
        {
            result.Role = role;
        }
        return result;
    }
}
using System.Security.Claims;

namespace Market.Auth.Application.Auth
{
    public interface IJwtHelper
    {
        string GenerateRefreshToken();
        string GenerateToken(string userName, TimeSpan expiryDuration);
        ClaimsPrincipal ValidateToken(string token);
    }
}
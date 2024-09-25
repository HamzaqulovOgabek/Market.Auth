using System.Security.Claims;

namespace Market.Auth.Application.Auth
{
    public interface IJwtHelper
    {
        string GenerateToken(string userName);
        ClaimsPrincipal ValidateToken(string token);
    }
}
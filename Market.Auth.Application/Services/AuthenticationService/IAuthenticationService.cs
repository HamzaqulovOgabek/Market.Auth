using Market.Auth.Application.Extensions;

namespace Market.Auth.Application.Services.AuthenticationService;

public interface IAuthenticationService
{
    Task<OperationResult> LoginAsync(UserLoginDto dto);
    Task<OperationResult> LogoutAsync(string token);
    Task<OperationResult> RefreshTokenAsync(string refreshToken);
    Task<OperationResult> RegisterUserAsync(UserRegistrationDto dto);
}
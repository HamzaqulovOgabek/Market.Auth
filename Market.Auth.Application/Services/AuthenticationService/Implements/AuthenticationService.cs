using Market.Auth.Application.Auth;
using Market.Auth.Application.Extensions;
using Market.Auth.Application.Services.UserServices;
using Market.Auth.DataAccess.Repositories.UserRepo;
using Market.Auth.DataAccess.Repositories.UserTokenRepository;
using Market.Auth.Domain.Models;
using Microsoft.Extensions.Options;

namespace Market.Auth.Application.Services.AuthenticationService;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _repository;
    private readonly IJwtHelper _jwtHelper;
    private readonly IUserTokenRepository _tokenRepository;
    private readonly JwtSettings _jwtSettings;

    public AuthenticationService(
        IUserRepository repository,
        IJwtHelper jwtHelper,
        IUserTokenRepository tokenRepository,
        IOptions<JwtSettings> jwtSettings
    )
    {
        _repository = repository;
        _jwtHelper = jwtHelper;
        _tokenRepository = tokenRepository;
        _jwtSettings = jwtSettings.Value;
    }
    public async Task<OperationResult> RegisterUserAsync(UserRegistrationDto dto)
    {
        //RegistrationDto fields are validated by Attributes in DTO class


        // Loop to check for race conditions and avoid duplicate registrations
        for (int i = 0; i < 20; i++)  // Retry a few times in case of concurrency issues
        {
            var existingUser = await _repository.GetUserByEmailOrUsernameAsync(dto.EmailOrUsername);
            if (existingUser != null)
            {
                return new OperationResult
                {
                    Success = false,
                    Errors = new List<string> { "UserName already exist. Please choose another." }
                };
            }
        }
        var user = (User)dto;
        await _repository.CreateAsync(user);

        return new OperationResult { Success = true };
    }
    public async Task<OperationResult> LoginAsync(UserLoginDto dto)
    {
        var user = await _repository.GetUserByEmailOrUsernameAsync(dto.EmailOrUsername);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            return await HandleFailedLoginAsync(user);
        }
        return await HandleSuccessfulLoginAsync(user);
    }
    public async Task<OperationResult> LogoutAsync(string token)
    {
        await _tokenRepository.RevokeTokenAsync(token);

        return new OperationResult { Success = true };

    }
    public async Task<UserBaseDto> GetMeAsync(int userId)
    {
        var user = await _repository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("User not found");
        }
        var userDto = new UserBaseDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName,
        };
        return userDto;
    }
    public async Task<OperationResult> RefreshTokenAsync(string refreshToken)
    {
        var userToken = await _tokenRepository.GetTokenAsync(refreshToken);
        if (userToken == null ||
            userToken.IsRevoked ||
            userToken.ExpiryDate > DateTime.Now)
        {
            return new OperationResult
            {
                Success = false,
                Errors = new List<string> { "Invalid or expired refresh token" }
            };
        }

        await _tokenRepository.RevokeTokenAsync(userToken.Token); //revoke old refresh token

        var newAccessToken = _jwtHelper.GenerateToken(userToken.UserId.ToString(), TimeSpan.FromMinutes(30));
        var refreshtoken = _jwtHelper.GenerateRefreshToken();

        userToken.Token = refreshToken;
        userToken.CreatedDate = DateTime.Now;
        userToken.ExpiryDate = DateTime.Now.AddDays(7);
        
        await _tokenRepository.AddTokenAsync(userToken);

        return new OperationResult
        {
            Success = true,
            AccessToken = newAccessToken,
            RefreshToken = refreshtoken
        };
    }
    private async Task<OperationResult> HandleFailedLoginAsync(User user)
    {
        if (user != null)
        {
            user.FailedLoginAttempts++;

            if (user.FailedLoginAttempts >= 5)
            {
                user.LockoutEndTime = DateTime.Now.AddMinutes(15); //lock user for 15 mins 
                user.FailedLoginAttempts = 0; //reset after lockout
                return new OperationResult
                {
                    Success = false,
                    Errors = new List<string> { "Try a few moments later" }
                };
            }
            await _repository.UpdateAsync(user);
        }

        return new OperationResult
        {
            Success = false,
            Errors = new List<string> { "Login Fails" }
        };
    }
    private async Task<OperationResult> HandleSuccessfulLoginAsync(User user)
    {
        if (user.LockoutEndTime.HasValue && user.LockoutEndTime.Value < DateTime.Now)
        {
            return new OperationResult
            {
                Success = false,
                Errors = new List<string> { "Try a few moments later" }
            };
        }

        user.FailedLoginAttempts = 0;
        user.LockoutEndTime = null;

        var accessToken = _jwtHelper.GenerateToken(user.Id.ToString(), new TimeSpan(15));
        var refreshToken = _jwtHelper.GenerateRefreshToken();

        if (string.IsNullOrWhiteSpace(accessToken))
        {
            return new OperationResult
            {
                Success = false,
                Errors = new List<string> { "Failed to generate JWT token" }
            };
        }
        var userToken = new UserToken
        {
            UserId = user.Id,
            Token = refreshToken,
            ExpiryDate = DateTime.Now.AddDays(7),
            IsRevoked = false,
            CreatedDate = DateTime.Now
        };

        await _tokenRepository.AddTokenAsync(userToken);

        return new OperationResult
        {
            Success = true,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}
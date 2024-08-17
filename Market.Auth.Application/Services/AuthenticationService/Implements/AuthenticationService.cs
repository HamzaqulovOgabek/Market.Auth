using Market.Auth.Application.Auth;
using Market.Auth.Application.Extensions;
using Market.Auth.Application.Services.UserServices;
using Market.Auth.DataAccess.Repositories.UserRepo;
using Market.Auth.DataAccess.Repositories.UserTokenRepository;
using Market.Auth.Domain.Exceptions;
using Market.Auth.Domain.Models;
using Microsoft.Extensions.Options;

namespace Market.Auth.Application.Services.AuthenticationService;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _repository;
    private readonly JwtHelper _jwtHelper;
    private readonly IUserTokenRepository _tokenRepository;
    private readonly JwtSettings _jwtSettings;

    public AuthenticationService(
        IUserRepository repository,
        JwtHelper jwtHelper,
        IUserTokenRepository tokenRepository,
        IOptions<JwtSettings> jwtSettings
    )
    {
        _repository = repository;
        _jwtHelper = jwtHelper;
        _tokenRepository = tokenRepository;
        this._jwtSettings = jwtSettings.Value;
    }

    public async Task<OperationResult> RegisterUserAsync(UserRegistrationDto dto)
    {

        throw new NotFoundException("XATO!!!");

        var existingUser = await _repository.GetUserByUsernameAsync(dto.UserName);
        if (existingUser != null)
        {
            return new OperationResult
            {
                Success = false,
                Errors = new List<string> { "UserName already exist" }
            };
        }
        var user = (User)dto;
        await _repository.CreateAsync(user);

        return new OperationResult { Success = true };
    }
    public async Task<OperationResult> LoginAsync(UserLoginDto dto)
    {
        var user = await _repository.GetUserByUsernameAsync(dto.UserName);
        if (user == null || user.PasswordHash != dto.PasswordHash)
        {
            return new OperationResult
            {
                Success = false,
                Errors = new List<string> { "Invalid username or password" }
            };
        }
        var token = _jwtHelper.GenerateToken(user.UserName);
        var userToken = new UserToken
        {
            UserId = user.Id,
            Token = token,
            ExpiryDate = DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes),
            IsRevoked = false,
            CreatedDate = DateTime.Now
        };
        await _tokenRepository.AddTokenAsync(userToken);

        return new OperationResult { Success = true, Token = token };
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
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName,
        };
        return userDto;
    }
}
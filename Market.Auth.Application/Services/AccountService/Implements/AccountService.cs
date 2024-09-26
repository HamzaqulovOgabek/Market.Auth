using Market.Auth.Application.Auth;
using Market.Auth.Application.Extensions;
using Market.Auth.Application.Services.UserServices;
using Market.Auth.DataAccess.Repositories.UserRepo;
using Market.Auth.Domain.Models;

namespace Market.Auth.Application.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly IUserRepository _repository;
    private readonly IEmailService _emailService;
    private readonly IJwtHelper _jwtHelper;

    public AccountService(
        IUserRepository repository, 
        IEmailService emailService, 
        IJwtHelper jwtHelper
    )
    {
        _repository = repository;
        _emailService = emailService;
        _jwtHelper = jwtHelper;
    }

    public async Task<OperationResult> RequestPasswordResetAsync(string email)
    {
        var user = await _repository.GetUserByEmailOrUsernameAsync(email);
        if (user == null)
        {
            return new OperationResult
            {
                Success = false,
                Errors = new List<string> { "User not found" }
            };
        }
        user.PasswordResetToken = _jwtHelper.GenerateRefreshToken();
        user.PasswordResetTokenExpiry = DateTime.Now.AddHours(1); // Set token expiry to 1 hour

        await _repository.UpdateAsync(user);


        await SendPasswordResetLinkToEmailAsync(user);

        return new OperationResult { Success = true };
    }
    public async Task<OperationResult> ResetPasswordAsync(string token, string newPassword)
    {
        var user = await _repository.GetUserByPasswordResetToken(token);

        if (user == null || user.PasswordResetTokenExpiry < DateTime.Now)
        {
            return new OperationResult
            {
                Success = false,
                Errors = new List<string> { "Invalid or Expired Token " }
            };
        }
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
        user.PasswordResetTokenExpiry = null;
        user.PasswordResetToken = null;

        await _repository.UpdateAsync(user);

        return new OperationResult { Success = true };
    }

    public async Task UpdateUsernameAsync(UsernameUpdateDto dto)
    {
        var user = await _repository.GetByIdAsync(dto.UserId);

        if (user == null || (user.UserName != dto.OldUsername || user.UserName != null))
        {
            throw new Exception("Something wrong");
        }

        var existingUsername = await _repository.GetUserByEmailOrUsernameAsync(dto.NewUsername);
        if (existingUsername != null)
            throw new Exception("Username is already in use. Please choose another.");

        user.UserName = dto.NewUsername;
        await _repository.UpdateAsync(user);
    }
    private async Task SendPasswordResetLinkToEmailAsync(User? user)
    {
        var resetLink = $"https://localhost:7101/account/reset-password?token={user.PasswordResetToken}";
        var emailContent = $"Hi {user.FirstName},<br/>" +
                           $"You requested a password reset. Please click the following link to reset your password:<br/>" +
                           $"<a href='{resetLink}'>{resetLink}</a><br/>" +
                           $"If you did not request a password reset, please ignore this email.";

        await _emailService.SendEmailAsync(user.Email, "Password Reset Request", emailContent);
    }
}

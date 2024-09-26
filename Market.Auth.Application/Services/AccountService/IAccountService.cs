using Market.Auth.Application.Extensions;

namespace Market.Auth.Application.Services.AccountService;

public interface IAccountService
{
    Task<OperationResult> RequestPasswordResetAsync(string email);
    Task<OperationResult> ResetPasswordAsync(string token, string newPassword);
}

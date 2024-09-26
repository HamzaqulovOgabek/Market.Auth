using Market.Auth.Application.Extensions;
using Market.Auth.Application.Services.UserServices;

namespace Market.Auth.Application.Services.AccountService;

public interface IAccountService
{
    Task<OperationResult> RequestPasswordResetAsync(string email);
    Task<OperationResult> ResetPasswordAsync(string token, string newPassword);
    Task UpdateUsernameAsync(UsernameUpdateDto dto);
}

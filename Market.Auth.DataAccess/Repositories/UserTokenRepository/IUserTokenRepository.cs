using Market.Auth.Domain.Models;

namespace Market.Auth.DataAccess.Repositories.UserTokenRepository
{
    public interface IUserTokenRepository
    {
        Task AddTokenAsync(UserToken token);
        Task<UserToken> GetTokenAsync(string token);
        Task RevokeTokenAsync(string token);
    }
}
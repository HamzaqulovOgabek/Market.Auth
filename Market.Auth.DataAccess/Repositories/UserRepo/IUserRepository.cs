using Market.Auth.DataAccess.Repositories.Base;
using Market.Auth.Domain.Models;

namespace Market.Auth.DataAccess.Repositories.UserRepo
{
    public interface IUserRepository : IBaseRepository<User, int>
    {
        Task<User?> GetUserByEmailOrUsernameAsync(string username);
        Task<User?> GetUserByPasswordResetToken(string token);
    }
}

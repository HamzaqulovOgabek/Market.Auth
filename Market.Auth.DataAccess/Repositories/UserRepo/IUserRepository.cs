using Market.Auth.DataAccess.Repositories.Base;
using Market.Auth.Domain.Models;

namespace Market.Auth.DataAccess.Repositories.UserRepo
{
    public interface IUserRepository : IBaseRepository<User, int>
    {
        Task<User?> GetUserByUsernameAsync(string username);
    }
}

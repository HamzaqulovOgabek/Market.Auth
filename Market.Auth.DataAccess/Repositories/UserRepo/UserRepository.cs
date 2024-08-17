using Market.Auth.Domain.Enums;
using Market.Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Auth.DataAccess.Repositories.UserRepo;

public class UserRepository : BaseRepository<User, int>, IUserRepository
{
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        var user = await Context.Users
            .Where(u => u.State == State.Active)
            .SingleOrDefaultAsync(u => u.UserName == username);
        
        return user;
    }
}

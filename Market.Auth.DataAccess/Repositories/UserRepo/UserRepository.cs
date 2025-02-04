using Market.Auth.Domain.Enums;
using Market.Auth.Domain.Exceptions;
using Market.Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Auth.DataAccess.Repositories.UserRepo;

public class UserRepository : BaseRepository<User, int>, IUserRepository
{
    public UserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<User?> GetUserByEmailOrUsernameAsync(string usernameOrEmail)
    {
        var user = await Context.Users
            .Where(u => u.State == State.Active)
            .SingleOrDefaultAsync(u => u.UserName == usernameOrEmail || u.Email == usernameOrEmail);
        
        return user;
    }

    public async Task<User?> GetUserByPasswordResetToken(string token)
    {
        var user = (await Context.UserTokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Token == token) ?? new())
            .User;
        return user;
    }
}

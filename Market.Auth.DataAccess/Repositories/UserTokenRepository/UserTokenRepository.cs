using Market.Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Auth.DataAccess.Repositories.UserTokenRepository;

public class UserTokenRepository : IUserTokenRepository
{
    private readonly AppDbContext _context;

    public UserTokenRepository(AppDbContext dbContext)
    {
        this._context = dbContext;
    }
    public async Task AddTokenAsync(UserToken token)
    {
        await _context.UserTokens.AddAsync(token);
        await _context.SaveChangesAsync();
    }
    public async Task<UserToken> GetTokenAsync(string token)
    {
        return await _context.UserTokens
            .SingleOrDefaultAsync(t => t.Token == token && !t.IsRevoked);
    }
    public async Task RevokeTokenAsync(string token)
    {
        var storedToken = await _context.UserTokens
            .SingleOrDefaultAsync(t => t.Token == token && !t.IsRevoked);

        if (storedToken != null)
        {
            storedToken.IsRevoked = true;
            storedToken.RevokedDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }

}

using Market.Auth.Domain.Models;

namespace Market.Auth.DataAccess.Repositories.PermissionRepo;

public class PermissionRepository : BaseRepository<Permission, int>, IPermissionRepository
{
    public PermissionRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}

using Market.Auth.Domain.Models;

namespace Market.Auth.DataAccess.Repositories.PermissionGroupRepo;

public class PermissionGroupRepository : BaseRepository<PermissionGroup, int>, IPermissionGroupRepository
{
    public PermissionGroupRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}

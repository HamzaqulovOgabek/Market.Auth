using Market.Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.Auth.DataAccess.Repositories.RoleRepo;

public class RoleRepository : BaseRepository<Role, int>, IRoleRepository
{
    public RoleRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    public async Task AddPermissionToRoleAsync (int roleId, int permissionId)
    {
        var rolePermission = new RolePermission
        {
            RoleId = roleId,
            PermissionId = permissionId
        };

        await Context.RolePermissions.AddAsync(rolePermission);
        await Context.SaveChangesAsync();
    }

    public async Task RemovePermissionFromRoleAsync(int roleId)
    {
        var rolePermissions = await Context.RolePermissions.Where(rp => rp.RoleId == roleId)
            .ToListAsync();
        
        Context.RolePermissions.RemoveRange(rolePermissions);
        await Context.SaveChangesAsync();
    }
}

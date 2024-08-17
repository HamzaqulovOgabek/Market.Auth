using Market.Auth.DataAccess.Repositories.Base;
using Market.Auth.Domain.Models;

namespace Market.Auth.DataAccess.Repositories.RoleRepo;

public interface IRoleRepository : IBaseRepository<Role, int>
{
    Task AddPermissionToRoleAsync(int roleId, int permissionId);
    Task RemovePermissionFromRoleAsync(int roleId);
}

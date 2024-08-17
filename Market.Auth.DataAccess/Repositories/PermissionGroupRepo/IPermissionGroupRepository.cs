using Market.Auth.DataAccess.Repositories.Base;
using Market.Auth.Domain.Models;

namespace Market.Auth.DataAccess.Repositories.PermissionGroupRepo;

public interface IPermissionGroupRepository : IBaseRepository<PermissionGroup, int>
{
}

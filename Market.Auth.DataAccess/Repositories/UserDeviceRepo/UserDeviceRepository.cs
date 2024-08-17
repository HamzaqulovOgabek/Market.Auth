using Market.Auth.Domain.Models;

namespace Market.Auth.DataAccess.Repositories.UserDeviceRepo;

public class UserDeviceRepository : BaseRepository<UserDevice, int>, IUserDeviceRepository
{
    public UserDeviceRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}

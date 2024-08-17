using Market.Auth.Application.Dto;
using Market.Auth.Application.Extensions;
using Market.Auth.DataAccess.Repositories.UserDeviceRepo;

namespace Market.Auth.Application.Services.UserDeviceServices;

public class UserDeviceService : IUserDeviceService
{
    private readonly IUserDeviceRepository repository;

    public UserDeviceService(IUserDeviceRepository repository)
    {
        this.repository = repository;
    }

    public async Task<UserDeviceBaseDto> GetAsync(int id)
    {
        var userDevice = await repository.GetByIdAsync(id);
        return new UserDeviceBaseDto
        {
            UserId = userDevice.UserId,
            Agent = userDevice.Agent,
            MacAddress = userDevice.MacAddress
        };
    }
    public IQueryable<UserDeviceBaseDto> GetList(BaseSortFilterDto options)
    {
        var userDevices = repository.GetAll()
            .SortFilter(options)
            .Select(x => new UserDeviceBaseDto
            {
                UserId = x.UserId,
                Agent = x.Agent,
                MacAddress = x.MacAddress
            });

        return userDevices;
    }

    public async Task<int> CreateAsync(UserDeviceBaseDto dto)
    {
        var createdUserDeviceId = await repository.CreateAsync(new()
        {
            UserId = dto.UserId,
            Agent = dto.Agent,
            MacAddress = dto.MacAddress,
        });

        return createdUserDeviceId;
    }
    public async Task<int> UpdatedAsync(UserDeviceUpdateDto dto)
    {
        var updatedUserDeviceId = await repository.UpdateAsync(new()
        {
            Id = dto.UserId,
            UserId = dto.UserId,
            Agent = dto.Agent,
            MacAddress = dto.MacAddress,
        });

        return updatedUserDeviceId;
    }

    public async Task DeleteAsync(int id)
    {
        await repository.DeleteAsync(id);
    }

}

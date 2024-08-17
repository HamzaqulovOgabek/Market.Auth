using Market.Auth.Application.Dto;

namespace Market.Auth.Application.Services.UserDeviceServices
{
    public interface IUserDeviceService
    {
        Task<int> CreateAsync(UserDeviceBaseDto dto);
        Task DeleteAsync(int id);
        Task<UserDeviceBaseDto> GetAsync(int id);
        IQueryable<UserDeviceBaseDto> GetList(BaseSortFilterDto options);
        Task<int> UpdatedAsync(UserDeviceUpdateDto dto);
    }
}

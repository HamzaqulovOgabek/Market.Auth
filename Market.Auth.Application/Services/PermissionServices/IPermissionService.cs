using Market.Auth.Application.Dto;

namespace Market.Auth.Application.Services.PermissionServices;

public interface IPermissionService
{
    IQueryable<PermissionBaseDto> GetAll(BaseSortFilterDto options);
    Task<PermissionBaseDto> GetAsync(int id);
    Task ResolvePermissionsAsync();
    //Task<int> UpdateAsync(PermissionUpdateDto dto);
}

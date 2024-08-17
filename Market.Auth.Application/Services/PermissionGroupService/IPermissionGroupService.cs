using Market.Auth.Application.Dto;

namespace Market.Auth.Application.Services.PermissionGroupService;

public interface IPermissionGroupService
{
    Task DeleteAsync(int id);
    Task<PermissionGroupBaseDto> GetAsync(int id);
    IQueryable<PermissionGroupBaseDto> GetList(BaseSortFilterDto options);
    Task ResolvePermissionGroupAsync();
}

using Market.Auth.Application.Dto;

namespace Market.Auth.Application.Services.RoleService;

public interface IRoleService
{
    Task<int> CreateAsync(RoleBaseDto dto);
    Task DeleteAsync(int id);
    Task<RoleBaseDto> GetAsync(int id);
    IQueryable<RoleBaseDto> GetList(BaseSortFilterDto options);
    Task<int> UpdateAsync(RoleUpdateDto dto);
}

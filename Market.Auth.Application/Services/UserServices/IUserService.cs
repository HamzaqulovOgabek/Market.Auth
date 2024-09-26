using Market.Auth.Application.Dto;
using Market.Auth.Application.Extensions;

namespace Market.Auth.Application.Services.UserServices;

public interface IUserService
{
    Task DeleteAsync(int id);
    Task<UserBaseDto> GetAsync(int id);
    IQueryable<UserBaseDto> GetList(BaseSortFilterDto options);
    Task<OperationResult> UpdateAsync(UserUpdateDto dto);
}

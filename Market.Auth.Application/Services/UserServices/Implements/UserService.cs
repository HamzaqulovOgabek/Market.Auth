using AutoMapper;
using Market.Auth.Application.Dto;
using Market.Auth.Application.Extensions;
using Market.Auth.DataAccess.Repositories.UserRepo;
using Market.Auth.Domain.Models;

namespace Market.Auth.Application.Services.UserServices;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<UserBaseDto> GetAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null)
        {
            return null;
        }
        return _mapper.Map<UserBaseDto>(user);
    }
    public IQueryable<UserBaseDto> GetList(BaseSortFilterDto options)
    {
        var users = _repository.GetAll()
            .SortFilter(options)
            .Select(x => _mapper.Map<UserBaseDto>(x));

        return users;
    }
    public async Task<OperationResult> UpdateAsync(UserUpdateDto dto)
    {
        var userDto = await GetAsync(dto.Id);

        userDto.UserName = dto.UserName ?? userDto.UserName;
        userDto.FirstName = dto.FirstName ?? userDto.FirstName;
        userDto.LastName = dto.LastName ?? userDto.LastName;
        userDto.MiddleName = dto.MiddleName ?? userDto.MiddleName;

        var updatedUserId = await _repository.UpdateAsync(_mapper.Map<User>(userDto));

        return new OperationResult { Success = true };
    }
    public async Task DeleteAsync(int id)
    {
        await GetAsync(id);
        await _repository.DeleteAsync(id);
    }
}

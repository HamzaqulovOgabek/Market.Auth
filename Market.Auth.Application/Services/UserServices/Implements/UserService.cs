using Market.Auth.Application.Dto;
using Market.Auth.Application.Extensions;
using Market.Auth.DataAccess.Repositories.UserRepo;

namespace Market.Auth.Application.Services.UserServices;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    //private readonly IJwtTokenGenerator jwtTokenGenerator;

    //public UserService(IUserRepository repository, IJwtTokenGenerator jwtTokenGenerator)
    //{
    //    this._repository = repository;
    //    this.jwtTokenGenerator = jwtTokenGenerator;
    //}
    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserBaseDto> GetAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null)
        {
            return null;
        }
        //What fields should I need to return
        return new UserBaseDto
        {
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName,
        };
    }

    public IQueryable<UserBaseDto> GetList(BaseSortFilterDto options)
    {
        var users = _repository.GetAll()
            .SortFilter(options)
            .Select(x => new UserBaseDto
            {
                UserName = x.UserName,
                FirstName = x.FirstName,
                LastName = x.LastName,
                MiddleName = x.MiddleName,
                //PasswordHash = x.PasswordHash,
                //Salt = x.Salt
            });
        return users;

    }

    public async Task<OperationResult> UpdateAsync(UserUpdateDto dto)
    {
        var user = await _repository.GetByIdAsync(dto.Id);
        if (user == null)
        {
            return new OperationResult
            {
                Success = false,
                Errors = new List<string> { "User not found" }
            };
        }

        user.UserName = dto.UserName ?? user.UserName;
        user.FirstName = dto.FirstName ?? user.FirstName;
        user.LastName = dto.LastName ?? user.LastName;
        user.MiddleName = dto.MiddleName ?? user.MiddleName;

        var updatedUserId = await _repository.UpdateAsync(user);

        return new OperationResult { Success = true };
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}

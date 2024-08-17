using Market.Auth.Application.Dto;
using Market.Auth.Application.Extensions;
using Market.Auth.DataAccess.Repositories.PermissionGroupRepo;
using Market.Auth.Domain.Enums;
using System.Reflection;
namespace Market.Auth.Application.Services.PermissionGroupService;

public class PermissionGroupService : IPermissionGroupService
{
    private readonly IPermissionGroupRepository _repository;

    public PermissionGroupService(IPermissionGroupRepository repository)
    {
        this._repository = repository;
    }

    public async Task<PermissionGroupBaseDto> GetAsync(int id)
    {
        var permissionGroup = await _repository.GetByIdAsync(id);
        if (permissionGroup == null)
        {
            throw new Exception("Permission Group not found");
        }
        return new PermissionGroupBaseDto
        {
            Code = permissionGroup.Code,
            Name = permissionGroup.Name,
        };
    }
    public IQueryable<PermissionGroupBaseDto> GetList(BaseSortFilterDto options)
    {
        var permissionGroups = _repository.GetAll()
            .SortFilter(options)
            .Select(x => new PermissionGroupBaseDto
            {
                Code = x.Code,
                Name = x.Name
            });

        if(permissionGroups == null)
        {
            throw new Exception("Permission groups not found");
        }
        return permissionGroups;
    }

    public async Task ResolvePermissionGroupAsync()
    {
        var enumPermissionGroup = typeof(PermissionGroup);
        var groups = enumPermissionGroup.GetFields(BindingFlags.Public | BindingFlags.Static);

        foreach (var group in groups)
        {
            if (group.IsLiteral)
            {
                var groupValue = (int) group.GetValue(null);
                var groupName = group.Name;

                var existingGroup = await _repository.GetByIdAsync(groupValue);
                if (existingGroup == null)
                {
                    var newGroup = new Domain.Models.PermissionGroup
                    {
                        Id = groupValue,
                        Code = groupName,
                        Name = groupName
                    };
                    await _repository.CreateAsync(newGroup);

                }

            }

        }
    }
    
    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}

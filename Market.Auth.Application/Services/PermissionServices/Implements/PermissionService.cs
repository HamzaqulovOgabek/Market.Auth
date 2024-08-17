using Market.Auth.Application.Dto;
using Market.Auth.Application.Extensions;
using Market.Auth.DataAccess;
using Market.Auth.DataAccess.Repositories.PermissionRepo;
using Market.Auth.Domain.Attributes;
using Market.Auth.Domain.Enums;
using System.Reflection;

namespace Market.Auth.Application.Services.PermissionServices;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _repository;
    private readonly AppDbContext _context;

    public PermissionService(
        IPermissionRepository repository,
        AppDbContext context)
    {
        this._repository = repository;
        _context = context;
    }

    public async Task<PermissionBaseDto> GetAsync(int id)
    {
        var permission = await _repository.GetByIdAsync(id);
        if (permission == null)
        {
            throw new Exception("Permission not found by this id");
        }

        return new PermissionBaseDto
        {
            Name = permission.Name,
            Code = permission.Code
        };
    }
    public IQueryable<PermissionBaseDto> GetAll(BaseSortFilterDto options)
    {
        var permission = _repository.GetAll()
            .SortFilter(options)
            .Select(x => new PermissionBaseDto
            {
                Name = x.Name,
                Code = x.Code
            });

        if (permission == null)
        {
            throw new Exception("Permissions not found");
        }

        return permission;
    }

    public async Task ResolvePermissionsAsync()
    {
        var enumPermissionType = typeof(Permission);
        var permissions = enumPermissionType.GetFields(BindingFlags.Public | BindingFlags.Static);

        foreach (var itemPermission in permissions)
        {
            if (itemPermission.IsLiteral)
            {
                var permissionValue = (int)itemPermission.GetValue(null);
                var permissionName = itemPermission.Name;

                var permissionGroupAttribute = itemPermission
                    .GetCustomAttributes(typeof(PermissionGroupName), false)
                    .FirstOrDefault() as PermissionGroupName;

                var permissionGroup = permissionGroupAttribute?.Group;

                var existingPermission = await _repository.GetByIdAsync(permissionValue);

                if (existingPermission == null)
                {
                    var newPermission = new Domain.Models.Permission
                    {
                        Id = permissionValue,
                        Name = permissionName,
                        Code = permissionName,
                        PermissionGroupId = (int)(permissionGroup ?? PermissionGroup.User)
                    };

                    await _repository.CreateAsync(newPermission);
                }
            }
        }
    }

    private async Task RemoveOutdatedPermissionsAsync()
    {
        var currentPermissions = _repository.GetAll();

        var enumPermissionType = typeof(Permission);
        var enumPermissions = enumPermissionType.GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.IsLiteral)
            .Select(f => f.GetValue(null))
            .ToHashSet();

        var permissionsToRemove = currentPermissions
            .Where(p => !enumPermissions.Contains(p.Id))
            .ToList();
        foreach (var permission in permissionsToRemove)
        {
            await _repository.DeleteAsync(permission.Id);
        }
    }

}

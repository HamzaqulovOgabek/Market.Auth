using Market.Auth.Application.Dto;
using Market.Auth.Application.Extensions;
using Market.Auth.DataAccess.Repositories.RoleRepo;
using Market.Auth.Domain.Models;
using System.Security.Cryptography.X509Certificates;

namespace Market.Auth.Application.Services.RoleService;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;

    public RoleService(IRoleRepository repository)
    {
        this._repository = repository;
    }

    public async Task<RoleBaseDto> GetAsync(int id)
    {
        var role = await _repository.GetByIdAsync(id);

        return new RoleBaseDto
        {
            Name = role.Name
        };
    }
    public IQueryable<RoleBaseDto> GetList(BaseSortFilterDto options)
    {
        var role = _repository.GetAll()
            .SortFilter(options)
            .Select(x => new RoleBaseDto
            {
                Name = x.Name
            });

        return role;
    }
    public async Task<int> CreateAsync(RoleBaseDto dto)
    {
        var createdRoleId = await _repository.CreateAsync(new() 
        {
            Name = dto.Name
        });
        
        if (dto.PermissionIds != null && dto.PermissionIds.Any())
        {
            foreach (var permissionId in dto.PermissionIds)
            {
                await _repository.AddPermissionToRoleAsync(createdRoleId, permissionId);
            }
        }
        return createdRoleId;
    }
    public async Task<int> UpdateAsync(RoleUpdateDto dto)
    {
        var existingRole = _repository.GetByIdAsync(dto.Id);
        if (existingRole == null)
        {
            throw new ArgumentNullException("Role not found");
        }

        var updatedRoleId = await _repository.UpdateAsync(new() 
        {
            Id = dto.Id,
            Name = dto.Name
        });

        await _repository.RemovePermissionFromRoleAsync(dto.Id);

        if(dto.PermissionIds != null && dto.PermissionIds.Any())
        {
            foreach (var permissionId in dto.PermissionIds)
            {
                await _repository.AddPermissionToRoleAsync(updatedRoleId, permissionId);
            }
        }
        return updatedRoleId;
    }
    public async Task DeleteAsync(int id)
    {
         await _repository.DeleteAsync(id);
    }
}

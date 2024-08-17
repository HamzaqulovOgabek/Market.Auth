using Market.Auth.Application.Dto;
using Market.Auth.Application.Services.RoleService;
using Market.Auth.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Auth.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class RoleController : ControllerBase
{
    private readonly IRoleService service;

    public RoleController(IRoleService service)
    {
        this.service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var role = await service.GetAsync(id);
        return Ok(role);
    }
    [HttpPost]
    public IActionResult GetList(BaseSortFilterDto options)
    {
        var roles = service.GetList(options);
        return Ok(roles);
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync(RoleBaseDto dto)
    {
        var createdRoleId = await service.CreateAsync(dto);
        return Ok(new { Id = createdRoleId });
    }
    [HttpPost]
    public async Task<IActionResult> UpdateAsync(RoleUpdateDto dto)
    {
        var updatedRoleId = await service.UpdateAsync(dto);
        return Ok(updatedRoleId);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await service.DeleteAsync(id);
        return Ok();
    }
}

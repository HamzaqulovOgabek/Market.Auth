using Market.Auth.Application.Dto;
using Market.Auth.Application.Services.PermissionGroupService;
using Microsoft.AspNetCore.Mvc;

namespace Market.Auth.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class PermissionGroupController : ControllerBase
{
    private readonly IPermissionGroupService _service;

    public PermissionGroupController(IPermissionGroupService service)
    {
        _service = service;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var permissionGroup = await _service.GetAsync(id);
        return Ok(permissionGroup);
    }
    [HttpPost]
    public IActionResult GetList(BaseSortFilterDto options)
    {
        var permissionGroups = _service.GetList(options);
        return Ok(permissionGroups);
    }
    [HttpDelete("{id}")]
    public async Task DeleteAsync(int id)
    {
        await _service.DeleteAsync(id);
    }
}

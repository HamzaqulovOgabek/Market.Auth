//using Market.Auth.Application.Dto;
//using Market.Auth.Application.Services.PermissionServices;
//using Microsoft.AspNetCore.Mvc;

//namespace Market.Auth.Controllers;

//[ApiController]
//[Route("api/[controller]/[action]")]
//public class PermissionController : ControllerBase
//{
//    private readonly IPermissionService _service;

//    public PermissionController(IPermissionService service)
//    {
//        _service = service;
//    }

//    [HttpGet("{id}")]
//    public async Task<IActionResult> GetAsync(int id)
//    {
//        var permission = await _service.GetAsync(id);
//        return Ok(permission);
//    }
//    [HttpPost]
//    public IActionResult GetList(BaseSortFilterDto options)
//    {
//        var permissions = _service.GetAll(options);
//        return Ok(permissions);
//    }
//    [HttpPost]
//    public async Task<IActionResult> CreateAsync(PermissionBaseDto dto)
//    {
//        var createdPermissionId = await _service.CreateAsync(dto);
//        return Ok(createdPermissionId);
//    }
//    [HttpPost]
//    public async Task<IActionResult> UpdateAsync(PermissionUpdateDto dto)
//    {
//        var updatedpermissionId = await _service.UpdateAsync(dto);
//        return Ok(updatedpermissionId);
//    }
//    [HttpDelete]
//    public async Task<IActionResult> DeleteAsync(int id)
//    {
//        await _service.DeleteAsync(id);
//        return Ok();
//    }
//}

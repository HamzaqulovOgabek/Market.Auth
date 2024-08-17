using Market.Auth.Application.Dto;
using Market.Auth.Application.Services.UserDeviceServices;
using Microsoft.AspNetCore.Mvc;

namespace Market.Auth.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserDeviceController : ControllerBase
{
    private readonly IUserDeviceService service;

    public UserDeviceController(IUserDeviceService service)
    {
        this.service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync (int id)
    {
        var userDevice = await service.GetAsync(id);
        return Ok(userDevice);
    }
    [HttpPost]
    public IActionResult GetList(BaseSortFilterDto options)
    {
        var userDevices = service.GetList(options);
        return Ok(userDevices);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateAsync(UserDeviceBaseDto dto)
    {
        var createdUserId = await service.CreateAsync(dto);
        return Ok(createdUserId);
    }
    [HttpPost]
    public async Task<IActionResult> UpdateAsync(UserDeviceUpdateDto dto)
    {
        var updatedUserId = await service.UpdatedAsync(dto);
        return Ok(updatedUserId);
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        service.DeleteAsync(id);
        return Ok();
    }

}

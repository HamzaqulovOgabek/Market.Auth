using Market.Auth.Application.Services.UserServices;
using Market.Auth.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Market.Auth.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        this._service = service;
    }

    [HttpGet("{id}")]
    [JwtAuthFilter]
    public async Task<IActionResult> GetAsync(int id)
    {
        var user = await _service.GetAsync(id);
        return Ok(user);
    }
    [HttpPost]
    //[ApiKeyAuthFilter]
    public async Task<IActionResult> UpdateAsync(UserUpdateDto dto)
    {
        var operationResult = await _service.UpdateAsync(dto);
        if (!operationResult.Success)
        {
            return BadRequest(operationResult.Errors);
        }
        return Ok(dto.Id);
    }

    [HttpDelete]
    //[ApiKeyAuthFilter]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}

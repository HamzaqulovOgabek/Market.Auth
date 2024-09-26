using System.Text;
using Market.Auth.Application.Services.AuthenticationService;
using Microsoft.AspNetCore.Mvc;

namespace Market.Auth.Controllers.Auth;
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _service;
    public AuthController(IAuthenticationService service)
    {
        this._service = service;
    }

    [HttpPost]
    [Route("/register")]
    public async Task<IActionResult> RegisterAsync(UserRegistrationDto dto)
    {
        var operationResult = await _service.RegisterUserAsync(dto);
        if (!operationResult.Success)
        {
            return BadRequest(operationResult.Errors);
        }

        return Ok();
    }

    [HttpPost]
    [Route("/login")]
    public async Task<IActionResult> LoginAsync(UserLoginDto dto)
    {
        var operationResult = await _service.LoginAsync(dto);
        if (!operationResult.Success)
        {
            return BadRequest(operationResult);
        }
        return Ok(operationResult);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> LogoutAsync(string token)
    {
        var result = await _service.LogoutAsync(token);

        if (result.Success)
        {
            return Ok("Logged out successfully");
        }

        return BadRequest(result.Errors);
    }
    
    [HttpPost("/refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync(string refreshToken)
    {
        var token = await _service.RefreshTokenAsync(refreshToken);
        return Ok(token);
    }

}

using Market.Auth.Application.Services.AccountService;
using Microsoft.AspNetCore.Mvc;

namespace Market.Auth.Controllers.Auth;
[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }
    [HttpPost]
    [Route("reset-password-request")]
    public async Task<IActionResult> RequestPasswordResetAsync(string email)
    {
        var result = await _service.RequestPasswordResetAsync(email);
        if(result.Success)
            return Ok();

        return BadRequest(result.Errors);
    }
    [HttpPost]
    [Route("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync(string token,[FromBody] string newPassword)
    {
        var result = await _service.ResetPasswordAsync(token, newPassword);
        if (result.Success)
            return Ok();

        return BadRequest(result.Errors);
    }
    [HttpGet]
    [Route("reset-password")]
    public IActionResult GetResetPasswordPage(string token)
    {
        // In a real application, you'd render a view or return a form.
        // For demonstration, let's just return the token.
        return Ok(new { Token = token }); // Example response
    }
}

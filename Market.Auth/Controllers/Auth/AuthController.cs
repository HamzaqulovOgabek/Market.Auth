using System.Text;
using Market.Auth.Application.Services.AuthenticationService;
using Microsoft.AspNetCore.Mvc;

namespace Market.Auth.Controllers.Auth;
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _service;


    /// <summary>
    /// This is a sample text file for download.
    /// </summary>
    /// <returns></returns>
    [HttpGet("download")]
    public async Task<IActionResult> DownloadFile()
    {
        string fileContent = "This is a sample text file for download.";
        byte[] fileBytes = Encoding.UTF8.GetBytes(fileContent);
        string fileName = "sample.txt";
        return File(fileBytes, "text/plain", fileName);
    }

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
            return BadRequest(operationResult.Errors);
        }
        return Ok(operationResult.Token);
    }
    //This method is really needed?
    //[HttpGet]
    //[Route("/verify")]
    //public IActionResult Verify(string token)
    //{
    //    var result = tokenManager.Verify(token);
    //    return Ok(result);
    //}
    //[HttpGet]
    //[Route("/getuserinfo")]
    //public IActionResult GetUserInfo(string token)
    //{
    //    var userName = tokenManager.GetUserInfoByToken(token);
    //    return Ok(userName);
    //}

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

}

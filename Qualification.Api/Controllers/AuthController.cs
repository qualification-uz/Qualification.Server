using Microsoft.AspNetCore.Mvc;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost]
    [Route("login")]
    public async ValueTask<IActionResult> Login([FromBody] UserLoginDto loginDto, [FromQuery] bool isExternal = false) 
        => Ok(await authService.LoginAsync(loginDto, isExternal));


    [HttpPost("register")]
    public async ValueTask<IActionResult> Register([FromBody] UserForCreationDto userDto) =>
        Ok(await authService.RegisterAsync(userDto));
}
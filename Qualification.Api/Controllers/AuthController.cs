using Microsoft.AspNetCore.Mvc;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;
    private readonly ITeacherService userService;

    public AuthController(IAuthService authService, ITeacherService userService)
    {
        this.authService = authService;
        this.userService = userService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto) 
        => Ok(await authService.LoginAsync(loginDto));
    
    
}
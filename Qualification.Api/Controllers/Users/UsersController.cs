using Microsoft.AspNetCore.Mvc;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers.Users;

[ApiController]
[Route("Api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ITeacherService _userService;

    public UsersController(ITeacherService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(UserForCreationDto dto)
    {
        return Ok(await _userService.CreateAsync(dto));
    }
    
}
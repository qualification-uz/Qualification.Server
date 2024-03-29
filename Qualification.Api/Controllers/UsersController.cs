﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Interfaces;

namespace Qualification.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Policy = "All")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("roles")]
        public IActionResult GetAllRoles()
            => Ok(this.userService.RetrieveAllRoles());

        /// <summary>
        /// If user is school, give login and password, else not
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("current")]
        public async Task<IActionResult> GetCurrentUser(UserLoginDto loginDto)
            => Ok(await this.userService.RetrieveCurrentUserAsync(loginDto.Login, loginDto.Password));

        /// <summary>
        /// Post user
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> PostUserAsync(UserForCreationDto userDto)
            => Ok(await this.userService.CreateUserAsync(userDto));
    }
}

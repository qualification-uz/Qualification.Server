using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Qualification.Domain.Entities.Users;
using Qualification.Domain.Enums;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Interfaces;
using System.Security.Claims;

namespace Qualification.Service.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> userManager;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IMapper mapper;

    public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;
        this.mapper = mapper;
    }

    public IEnumerable<RoleDto> RetrieveAllRoles()
    {
        var roleIds = Enum.GetValues<UserRole>();

        foreach (var roleId in roleIds)
            yield return new RoleDto {
                Id = (int)roleId,
                Name = Enum.GetName<UserRole>(roleId) };
    }

    public async ValueTask<UserDto> RetrieveCurrentUserAsync()
    {
        var userId = this.httpContextAccessor?
            .HttpContext?
            .User?
            .FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
            throw new InvalidOperationException("Couldn't find user id");

        var user = await this.userManager.FindByIdAsync(userId);

        return this.mapper.Map<UserDto>(user);
    }
}

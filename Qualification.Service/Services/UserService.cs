using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Qualification.Domain.Entities.Users;
using Qualification.Domain.Enums;
using Qualification.Service.AvloniyClient;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Exceptions;
using Qualification.Service.Interfaces;
using System.Security.Claims;

namespace Qualification.Service.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> userManager;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IMapper mapper;
    private readonly IAvloniyClientService avloniyClientService;

    public UserService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IMapper mapper, IAvloniyClientService avloniyClientService)
    {
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;
        this.mapper = mapper;
        this.avloniyClientService = avloniyClientService;
    }

    public IEnumerable<RoleDto> RetrieveAllRoles()
    {
        var roleIds = Enum.GetValues<UserRole>();

        foreach (var roleId in roleIds)
            yield return new RoleDto
            {
                Id = (int)roleId,
                Name = Enum.GetName<UserRole>(roleId)
            };
    }

    public async ValueTask<object> RetrieveCurrentUserAsync(string username, string password)
    {
        var userId = this.httpContextAccessor?
            .HttpContext?
            .User?
            .FindFirstValue(ClaimTypes.NameIdentifier);

        var role = this.httpContextAccessor?
            .HttpContext?
            .User?
            .FindFirstValue(ClaimTypes.Role).ToLower();

        if (userId is null)
            throw new InvalidOperationException("Couldn't find user id");

        if (role == "school")
        {
            var erpResponse = await this.avloniyClientService.IsUserRegistered(username, password);
            if (!erpResponse.Success)
                throw new InvalidOperationException("Couldn't find user in ERP");

            return erpResponse.Result;
        }

        var user = await this.userManager.FindByIdAsync(userId);

        return this.mapper.Map<UserDto>(user);
    }
    
    private static User GenerateNewUser(UserForCreationDto teacherDto)
    {
        return new User()
        {
            FirstName = teacherDto.FirstName,
            LastName = teacherDto.LastName,
            MiddleName = teacherDto.MiddleName,
            PhoneNumber = teacherDto.PhoneNumber,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = teacherDto.Login
        };
    }

    public async ValueTask<UserDto> CreateUserAsync(UserForCreationDto userDto)
    {
        var userExists = await this.userManager.FindByNameAsync(userDto.Login);

        if (userExists is not null)
            throw new AlreadyExistsException("User already exists with this login");

        User user = GenerateNewUser(userDto);
        
        var result = await this.userManager.CreateAsync(user, userDto.Password);

        if (!result.Succeeded)
            throw new InvalidOperationException(message:
                string.Join("", result.Errors.Select(error => error.Description)));

        await this.userManager.AddToRoleAsync(user, Enum.GetName(userDto.RoleId));
        
        var mappedUser = this.mapper.Map<UserDto>(user);
        mappedUser.RoleId = userDto.RoleId;
        mappedUser.Role = Enum.GetName(userDto.RoleId);

        return mappedUser;
    }
}

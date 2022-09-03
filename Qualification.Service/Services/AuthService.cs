using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Qualification.Domain.Entities.Users;
using Qualification.Service.AvloniyClient;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Exceptions;
using Qualification.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Qualification.Domain.Enums;

namespace Qualification.Service.Services;

public class AuthService : IAuthService
{
    private readonly IAvloniyClientService avloniyClientService;
    private readonly UserManager<User> userManager;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    private readonly RoleManager<Role> roleManager;

    public AuthService(
        IAvloniyClientService avloniyClientService,
        UserManager<User> userManager,
        IMapper mapper,
        IConfiguration configuration,
        RoleManager<Role> roleManager)
    {
        this.avloniyClientService = avloniyClientService;
        this.userManager = userManager;
        this.mapper = mapper;
        this.configuration = configuration;
        this.roleManager = roleManager;
    }

    public async ValueTask<object> LoginAsync(
        UserLoginDto loginDto,
        bool isExternalUser)
    {
        #region External API User

        if (isExternalUser)
        {
            var eRPReponse =
                await avloniyClientService.IsUserRegistered(
                    username: loginDto.Login,
                    password: loginDto.Password);

            // if response is null it causes not found exception
            if (!eRPReponse?.Success ?? true)
                throw new NotFoundException("Coudn't find user for given credentials");

            var school = this.mapper.Map<School>(eRPReponse.Result);

            if (school is null)
                throw new NullReferenceException("School object is null");

            return GenerateJwtToken(GetClaims(school, new[] { Enum.GetName(UserRole.School) }));
        }

        #endregion

        var user = await this.userManager.FindByNameAsync(loginDto.Login);

        if (user is null)
            throw new NotFoundException("Coudn't find user for given credentials.");

        bool isValidPassword = await this.userManager
            .CheckPasswordAsync(user, loginDto.Password);

        if (!isValidPassword)
            throw new NotFoundException("Coudn't find user for given credentials.");

        var roles = await this.userManager.GetRolesAsync(user);

        return GenerateJwtToken(GetClaims(user, roles));
    }

    public async ValueTask<UserDto> RegisterAsync(UserForCreationDto userDto)
    {
        var userExists = await userManager.FindByNameAsync(userDto.Login);

        if (userExists is not null)
            throw new AlreadyExistsException("Username has already taken.");

        if (!IsValidRole(userDto.RoleId))
            throw new InvalidOperationException("Given role is not valid");
        
        User user = GenerateNewUser(userDto);
        var result = await userManager.CreateAsync(user, userDto.Password);

        if (!result.Succeeded)
            throw new InvalidOperationException(message:
                string.Join("", result.Errors.Select(error => error.Description)));

        await CreateRolesAsync();
        await userManager.AddToRoleAsync(user, Enum.GetName(userDto.RoleId));

        var mappedUser = this.mapper.Map<UserDto>(user);
        mappedUser.RoleId = userDto.RoleId;
        mappedUser.Role = Enum.GetName(userDto.RoleId);

        return mappedUser;
    }

    private static User GenerateNewUser(UserForCreationDto userDto)
    {
        return new User()
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            MiddleName = userDto.MiddleName,
            PhoneNumber = userDto.PhoneNumber,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = userDto.Login
        };
    }

    private bool IsValidRole(UserRole role) =>
        role == UserRole.School ||
        role == UserRole.Student ||
        role == UserRole.Admin ||
        role == UserRole.SuperAdmin ||
        role == UserRole.Teacher ||
        role == UserRole.Tester;

    private async Task CreateRolesAsync()
    {
        var roles = Enum.GetNames(typeof(UserRole));

        for (var index = 0; index < roles.Length; index++)
            if (!await this.roleManager.RoleExistsAsync(roles[index]))
                await this.roleManager.CreateAsync(new Role(roles[index]));
    }

    #region JWT Token Generation

    private static List<Claim> GetClaims(User user, IList<string> userRoles)
    {
        var authClaims =
            new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim("role", userRole));
        }

        return authClaims;
    }

    private object GenerateJwtToken(IReadOnlyList<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(this.configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: this.configuration["JWT:ValidIssuer"],
            audience: this.configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(
                key: authSigningKey,
                algorithm: SecurityAlgorithms.HmacSha256)
            );

        return new
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = token.ValidTo
        };
    }

    #endregion
}
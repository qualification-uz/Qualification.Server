using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Users;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Exceptions;
using Qualification.Service.Extensions;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IConfiguration config, IUnitOfWork unitOfWork)
    {
        _config = config;
        _unitOfWork = unitOfWork;
    }

    private string CreateToken(BaseUser user)
    {
        var claims = new Claim[]
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.ToString()),
            new(ClaimTypes.Email, user.Login)
        };

        var issuer = _config.GetSection("JWT:issuer").Value;
        var expires = _config.GetSection("JWT:expires").Value;
        var secret = _config.GetSection("JWT:secret").Value;

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken(issuer, claims: claims, expires: DateTime.Now.AddDays(double.Parse(expires)),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> LoginAsync(UserLoginDto loginDto, bool externalUser = false)
    {
        loginDto.Password = loginDto.Password.Encrypt();

        if (externalUser)
        {
            // ToDo Check from ERP system
            throw new NotImplementedException();
        }

        var existUser = await _unitOfWork.Teachers.FirstOrDefaultAsync(user => 
            user.Login.Equals(loginDto.Login) && user.Password.Equals(loginDto.Password));

        if (existUser is null)
            throw new HttpStatusCodeException(401, "Wrong login or password!");

        return CreateToken(existUser);
    }

}
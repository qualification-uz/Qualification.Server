using Qualification.Service.DTOs.Users;

namespace Qualification.Service.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(UserLoginDto loginDto, bool externalUser = false);
}
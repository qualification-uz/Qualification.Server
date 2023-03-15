using Qualification.Service.DTOs.Users;
using System.Security.Claims;

namespace Qualification.Service.Interfaces;

public interface IAuthService
{
    ValueTask<object> LoginAsync(
        UserLoginDto loginDto,
        bool isExternalUser);

    ValueTask<UserDto> RegisterAsync(UserForCreationDto userDto);
    object GenerateJwtToken(IReadOnlyList<Claim> authClaims);
}
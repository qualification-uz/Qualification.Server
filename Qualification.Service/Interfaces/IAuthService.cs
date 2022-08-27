using Qualification.Service.DTOs.Users;

namespace Qualification.Service.Interfaces;

public interface IAuthService
{
    ValueTask<object> LoginAsync(
        UserLoginDto loginDto,
        bool isExternalUser);

    ValueTask<UserDto> RegisterAsync(UserForCreationDto userDto);
}
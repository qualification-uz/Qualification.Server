using Qualification.Service.DTOs.Users;

namespace Qualification.Service.Interfaces;

public interface IUserService
{
    IEnumerable<RoleDto> RetrieveAllRoles();
    ValueTask<UserDto> RetrieveCurrentUserAsync();
}

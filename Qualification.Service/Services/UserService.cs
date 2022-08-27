using Qualification.Domain.Enums;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services;

public class UserService : IUserService
{
    public IEnumerable<RoleDto> RetrieveAllRoles()
    {
        var roleIds = Enum.GetValues<UserRole>();

        foreach (var roleId in roleIds)
            yield return new RoleDto {
                Id = (int)roleId,
                Name = Enum.GetName<UserRole>(roleId) };
    }
}

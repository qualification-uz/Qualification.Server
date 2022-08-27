using Microsoft.AspNetCore.Identity;

namespace Qualification.Domain.Entities.Users;

public class Role : IdentityRole<long>
{
    public Role()
    { }

    public Role(string roleName)
        : base(roleName)
    { }
}

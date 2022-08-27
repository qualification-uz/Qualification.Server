using Microsoft.AspNetCore.Identity;

namespace Qualification.Domain.Entities.Users;

public class User : IdentityUser<long>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }

    public string Asset { get; set; }
}

using Qualification.Domain.Commons;
using Qualification.Domain.Enums;

namespace Qualification.Domain.Entities.Users;

public abstract class BaseUser : Auditable
{
    /// <summary>
    /// Login can be PINFL for teacher
    /// </summary>
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    public UserRole Role { get; set; }
}
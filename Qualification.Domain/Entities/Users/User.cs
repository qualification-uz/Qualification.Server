using Qualification.Domain.Commons;
using Qualification.Domain.Enums;

namespace Qualification.Domain.Entities.Users;

public class User : Auditable
{
    public string? Pinfl { get; set; } = string.Empty;
    public string? Firstname { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? FamilyName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
    
    public UserRole Role { get; set; }
}
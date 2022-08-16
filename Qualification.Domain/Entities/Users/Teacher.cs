namespace Qualification.Domain.Entities.Users;

public class Teacher : BaseUser
{
    public string? Firstname { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? FamilyName { get; set; } = string.Empty;
    public DateTime? DateOfBirth { get; set; }
}
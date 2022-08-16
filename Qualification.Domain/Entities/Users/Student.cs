namespace Qualification.Domain.Entities.Users;

public class Student : BaseUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
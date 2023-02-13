namespace Qualification.Domain.Entities.Users;

public class Student
{
    public long Id { get; set; }
    public long ApplicationId { get; set; }
    public Application Application { get; set; }
    public string PasswordHash { get; set; }
}
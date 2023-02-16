namespace Qualification.Domain.Entities.Users;

public class Student
{
    public long Id { get; set; }
    public long ApplicationId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public long? GradeId { get; set; }
    public string GradeLetter { get; set; }
    public Application Application { get; set; }
    public string PasswordHash { get; set; }
}
using Qualification.Domain.Commons;

namespace Qualification.Domain.Entities.Users;

public class Student : Auditable
{
    public string FirstName { get; set; }
    public long ERPId { get; set; }
    
    public string LastName { get; set; }
    
    public string MiddleName { get; set; }
    
    public long? GradeId { get; set; }
    public string GradeLetter { get; set; }
    
    public long? ApplicationId { get; set; }
    public Application Application { get; set; }
    
    public string PasswordHash { get; set; }
}
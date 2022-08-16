using System.ComponentModel.DataAnnotations;
using Qualification.Domain.Enums;

namespace Qualification.Service.DTOs.Users;

public class UserForCreationDto
{
    [Required] 
    public string FirstName { get; set; }

    [Required] 
    public string LastName { get; set; }
    
    [Required] 
    public string Login { get; set; }

    [Required] 
    public string Password { get; set; }
    
    [Required]
    public UserRole Role { get; set; }
}
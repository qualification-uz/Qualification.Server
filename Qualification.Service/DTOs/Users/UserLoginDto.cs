using System.ComponentModel.DataAnnotations;

namespace Qualification.Service.DTOs.Users;

public class UserLoginDto
{
    [Required]
    public string Login { get; set; }
    
    [Required]
    public string Password { get; set; }
}
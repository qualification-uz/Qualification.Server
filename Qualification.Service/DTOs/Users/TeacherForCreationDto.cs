using System.ComponentModel.DataAnnotations;

namespace Qualification.Service.DTOs.Users;

public class TeacherForCreationDto
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public string MiddleName { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public string PINFL { get; set; }

    [Required]
    public string Password { get; set; }
}

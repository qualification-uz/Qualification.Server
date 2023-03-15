using Qualification.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Qualification.Service.DTOs.Users;

public class UserForCreationDto
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public string PhoneNumber { get; set; }

    [Required]
    public string Login { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public UserRole RoleId { get; set; }
}
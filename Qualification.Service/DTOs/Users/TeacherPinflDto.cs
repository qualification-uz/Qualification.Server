using System.ComponentModel.DataAnnotations;

namespace Qualification.Service.DTOs.Users
{
    public class TeacherPinflDto
    {
        [Required]
        public string PINFL { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

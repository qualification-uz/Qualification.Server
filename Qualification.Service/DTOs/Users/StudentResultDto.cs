using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qualification.Domain.Entities;

namespace Qualification.Service.DTOs.Users
{
    public class StudentResultDto
    {
        public long Id { get; set; }
        public long ApplicationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public long? GradeId { get; set; }
        public string GradeLetter { get; set; }
    }
}

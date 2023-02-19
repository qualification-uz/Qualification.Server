using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Users;
using Qualification.Domain.Enums;
using Qualification.Service.AvloniyClient;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Exceptions;
using Qualification.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Qualification.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IApplicationRepository applicationRepository;
        private readonly ISchoolRepository schoolRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IAuthService authService;
        private readonly IMapper mapper;

        public StudentService(IApplicationRepository applicationRepository, 
            ISchoolRepository schoolRepository,
            IMapper mapper, 
            IStudentRepository studentRepository, 
            IAuthService authService)
        {
            this.applicationRepository = applicationRepository;
            this.schoolRepository = schoolRepository;
            this.mapper = mapper;
            this.studentRepository = studentRepository;
            this.authService = authService;
        }

        public async ValueTask<IEnumerable<Student>> RetrieveAllAsync(long schoolId, long applicationId)
        {
            var school = await this.applicationRepository.SelectAllApplications()
                .FirstOrDefaultAsync(p => p.SchoolId == schoolId);
            if (school == null)
                throw new Exception("School not found");

            var application = await this.applicationRepository.SelectApplicationByIdAsync(applicationId,
                includes: new List<string> { "Students" });
            if (application == null)
                throw new Exception("Application not found");
            
            return application.Students;
        }

        public async ValueTask<object> RetrieveByPasswordAsync(string password)
        {
            var studentExist = await studentRepository.SelectAllStudents()
                .FirstOrDefaultAsync(student => student.PasswordHash == password);
            if (studentExist is null)
                throw new NotFoundException("Coudn't find student for given credentials.");

            // TODO: Get claims from student
            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, studentExist.Id.ToString()),
                new Claim(ClaimTypes.Role, Enum.GetName(UserRole.Student)),
            };
            var token = this.authService.GenerateJwtToken(authClaims);

            return new
            {
                Student = mapper.Map<StudentResultDto>(studentExist),
                Auth = token
            };
        }
    }
}

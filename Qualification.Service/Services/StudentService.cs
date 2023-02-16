using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Users;
using Qualification.Service.DTOs.Users;
using Qualification.Service.Exceptions;
using Qualification.Service.Interfaces;

namespace Qualification.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IApplicationRepository applicationRepository;
        private readonly ISchoolRepository schoolRepository;
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentService(IApplicationRepository applicationRepository, ISchoolRepository schoolRepository,
            IMapper mapper, IStudentRepository studentRepository)
        {
            this.applicationRepository = applicationRepository;
            this.schoolRepository = schoolRepository;
            this.mapper = mapper;
            this.studentRepository = studentRepository;
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

        public async ValueTask<StudentResultDto> RetrieveByPasswordAsync(string password)
        {
            var student = await studentRepository.SelectAllStudents()
                .FirstOrDefaultAsync(student => student.PasswordHash == password);
            if (student == null)
                throw new NotFoundException("Student not found");

            return mapper.Map<StudentResultDto>(student);
        }
    }
}

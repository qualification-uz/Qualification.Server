using Qualification.Domain.Entities.Questions;
using Qualification.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualification.Data.IRepositories
{
    public interface IStudentRepository
    {
        IQueryable<Student> SelectAllStudents();
        ValueTask<Student> SelectStudentByIdAsync(long studentId);

        ValueTask<Student> SelectStudentByIdAsync(
            long studentId,
            IReadOnlyList<string> includes);

        ValueTask<Student> InsertStudentAsync(Student question);
        ValueTask<Student> UpdateStudentAsync(Student question);
        ValueTask<Student> DeleteStudentAsync(Student question);
    }
}

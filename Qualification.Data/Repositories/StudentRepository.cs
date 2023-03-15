using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Qualification.Data.Contexts;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Questions;
using Qualification.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualification.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext appDbContext;

        public StudentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IQueryable<Student> SelectAllStudents() => this.appDbContext.Student;

        public async ValueTask<Student> SelectStudentByIdAsync(long studentId) =>
            await this.appDbContext.Student.FindAsync(studentId);

        public async ValueTask<Student> SelectStudentByIdAsync(
            long studentId,
            IReadOnlyList<string> includes)
        {
            IQueryable<Student> students = this.appDbContext.Student;

            foreach (var include in includes)
                students = students.Include(include);

            return await students
                .FirstOrDefaultAsync(student => student.Id == studentId);
        }

        public async ValueTask<Student> InsertStudentAsync(Student student)
        {
            EntityEntry<Student> studentEntityEntry =
                await this.appDbContext.Student.AddAsync(student);

            await this.appDbContext.SaveChangesAsync();

            return studentEntityEntry.Entity;
        }

        public async ValueTask<Student> UpdateStudentAsync(Student student)
        {
            EntityEntry<Student> studentEntityEntry =
                this.appDbContext.Student.Update(student);

            await this.appDbContext.SaveChangesAsync();

            return studentEntityEntry.Entity;
        }

        public async ValueTask<Student> DeleteStudentAsync(Student student)
        {
            EntityEntry<Student> studentEntityEntry =
                this.appDbContext.Student.Remove(student);

            await this.appDbContext.SaveChangesAsync();

            return studentEntityEntry.Entity;
        }
    }
}

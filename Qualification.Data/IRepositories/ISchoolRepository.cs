using Qualification.Domain.Entities.Users;

namespace Qualification.Data.IRepositories
{
    public interface ISchoolRepository
    {
        IQueryable<School> SelectAllSchools();
        ValueTask<School> SelectSchoolByIdAsync(long schoolId);
        ValueTask<School> InsertSchoolAsync(School school);
    }
}

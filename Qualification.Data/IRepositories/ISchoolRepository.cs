using Qualification.Domain.Entities.Quizes;
using Qualification.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualification.Data.IRepositories
{
    public interface ISchoolRepository
    {
        IQueryable<School> SelectAllSchools();
        ValueTask<School> SelectSchoolByIdAsync(long schoolId);
        ValueTask<School> InsertSchoolAsync(School school);
    }
}

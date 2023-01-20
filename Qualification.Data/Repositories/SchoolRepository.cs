using Microsoft.EntityFrameworkCore.ChangeTracking;
using Qualification.Data.Contexts;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qualification.Data.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly AppDbContext appDbContext;

        public SchoolRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async ValueTask<School> InsertSchoolAsync(School school)
        {
            //EntityEntry<School> schoolEntityEntry =
            //    await this.appDbContext.Schools.AddAsync(school);

            //await this.appDbContext.SaveChangesAsync();

            //return schoolEntityEntry.Entity;

            return null;
        }

        public IQueryable<School> SelectAllSchools() => null;

        public async ValueTask<School> SelectSchoolByIdAsync(long schoolId) => null;
    }
}

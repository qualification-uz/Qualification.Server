using Microsoft.EntityFrameworkCore.ChangeTracking;
using Qualification.Data.Contexts;
using Qualification.Data.IRepositories;
using Qualification.Domain.Entities;

namespace Qualification.Data.Repositories;

public class ApplicationRepository : IApplicationRepository
{
    private readonly AppDbContext appDbContext;

    public ApplicationRepository(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async ValueTask<Application> InsertApplicationAsync(Application application)
    {
        EntityEntry<Application> applicationEntityEntry =
            await this.appDbContext.Applications.AddAsync(application);

        await this.appDbContext.SaveChangesAsync();

        return applicationEntityEntry.Entity;
    }

    public IQueryable<Application> SelectAllApplications() =>
        this.appDbContext.Applications;

    public async ValueTask<Application> SelectApplicationByIdAsync(long applicationId) =>
        await this.appDbContext.Applications.FindAsync(applicationId);

    public async ValueTask<Application> UpdateApplicationAsync(Application application)
    {
        EntityEntry<Application> applicationEntityEntry =
            this.appDbContext.Applications.Update(application);

        await this.appDbContext.SaveChangesAsync();

        return applicationEntityEntry.Entity;
    }
    
    public async ValueTask<Application> DeleteApplicationAsync(Application application)
    {
        EntityEntry<Application> applicationEntityEntry =
            this.appDbContext.Applications.Remove(application);

        await this.appDbContext.SaveChangesAsync();

        return applicationEntityEntry.Entity;
    }
}

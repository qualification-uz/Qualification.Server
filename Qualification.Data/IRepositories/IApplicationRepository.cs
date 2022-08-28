using Qualification.Domain.Entities;

namespace Qualification.Data.IRepositories;

public interface IApplicationRepository
{
    ValueTask<Application> InsertApplicationAsync(Application application);
    IQueryable<Application> SelectAllApplications();
    ValueTask<Application> SelectApplicationByIdAsync(long applicationId);
    ValueTask<Application> UpdateApplicationAsync(Application application);
    ValueTask<Application> DeleteApplicationAsync(Application application);
}

using Qualification.Domain.Entities.Users;

namespace Qualification.Data.IRepositories;

public interface IUnitOfWork: IDisposable
{
    IGenericRepository<Teacher> Teachers { get; }
    Task SaveChangesAsync();
}
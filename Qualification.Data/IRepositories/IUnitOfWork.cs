using Qualification.Domain.Entities.Users;

namespace Qualification.Data.IRepositories;

public interface IUnitOfWork: IDisposable
{
    IGenericRepository<User> Users { get; }
    Task SaveChangesAsync();
}
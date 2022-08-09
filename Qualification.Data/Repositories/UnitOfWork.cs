using Qualification.Data.IRepositories;
using Qualification.Data.Contexts;
using Qualification.Domain.Entities.Users;

namespace Qualification.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    
    public IGenericRepository<User> Users { get; }

    public UnitOfWork(AppDbContext dbContext)
    {
        Users = new GenericRepository<User>(dbContext);
        _dbContext = dbContext;
    }
    
    public void Dispose() => GC.SuppressFinalize(this);

    public Task SaveChangesAsync() => _dbContext.SaveChangesAsync();
}
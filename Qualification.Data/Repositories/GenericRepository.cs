using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Qualification.Data.IRepositories;
using Qualification.Data.Contexts;

namespace Qualification.Data.Repositories;

public class GenericRepository<TSource> : IGenericRepository<TSource> where TSource : class
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<TSource> _dbSet;
    
    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TSource>();
    }

    public async Task<TSource> AddAsync(TSource source) 
        => (await _dbSet.AddAsync(source)).Entity;

    public Task AddRangeAsync(IEnumerable<TSource> sources)
        => _dbSet.AddRangeAsync(sources);
    
    public TSource Update(TSource source) 
        => _dbSet.Update(source).Entity;

    public Task<TSource?> FirstOrDefaultAsync(Expression<Func<TSource, bool>> expression) 
        => _dbSet.FirstOrDefaultAsync(expression);

    public IQueryable<TSource> Where(Expression<Func<TSource, bool>>? expression = null)
        => expression is null ? _dbSet : _dbSet.Where(expression); 

    public async Task DeleteAsync(Expression<Func<TSource, bool>> expression)
    {
        var entityies = _dbSet.Where(expression);

        _dbSet.RemoveRange(entityies);
    }
}
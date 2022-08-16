using System.Linq.Expressions;

namespace Qualification.Data.IRepositories;

public interface IGenericRepository<TSource> where TSource : class
{
    Task<TSource> AddAsync(TSource source);
    Task AddRangeAsync(IEnumerable<TSource> sources);
    TSource Update(TSource source);
    Task<TSource?> FirstOrDefaultAsync(Expression<Func<TSource, bool>> expression);
    IQueryable<TSource> Where(Expression<Func<TSource, bool>>? expression = null);
    Task DeleteAsync(Expression<Func<TSource, bool>> expression);
}
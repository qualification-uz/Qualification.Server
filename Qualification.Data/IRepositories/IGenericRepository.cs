using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Qualification.Data.IRepositories;

public interface IGenericRepository<TSource> where TSource : class
{
    ValueTask<EntityEntry<TSource>> CreateAsync(TSource source);
    TSource Update(TSource source);
    Task<TSource?> GetAsync(Expression<Func<TSource, bool>> expression);
    IQueryable<TSource> Where(Expression<Func<TSource, bool>>? expression = null);
    Task<bool> DeleteAsync(Expression<Func<TSource, bool>> expression);
}
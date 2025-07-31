using System.Linq.Expressions;

namespace Domain.Interface;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> Query(
        Expression<Func<IQueryable<T>, IQueryable<T>>> query,
        int? limit = null,
        params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, int? limit = null);
    Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}

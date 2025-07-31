using System.Linq.Expressions;
using Domain.Interface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    internal AppDbContext dbContext;
    internal DbSet<TEntity> dbSet;

    public Repository(AppDbContext context)
    {
        dbContext = context;
        dbSet = context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity) => await dbSet.AddAsync(entity);

    public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate) => await dbSet.FirstAsync(predicate);

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate, int? limit)
    {
        var query = dbSet.Where(predicate);

        if (limit.HasValue)
            query = query.Take(limit.Value);

        return query.ToList();
    } 

    public async Task<TEntity?> GetByIdAsync(int id) => await dbSet.FindAsync(id);

    public async Task<IEnumerable<TEntity>> Query(
        Expression<Func<IQueryable<TEntity>, IQueryable<TEntity>>> query,
        int? limit = null,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> baseQuery = dbSet;
        // Применяем все Include, если они переданы
        foreach (var include in includes)
        {
            baseQuery = baseQuery.Include(include);
        }
        var result = query.Compile()(baseQuery);

        if (limit.HasValue)
            result = result.Take(limit.Value);

        return result.ToList();
    }

    public void Remove(TEntity entity) => dbSet.Remove(entity);

    public void Update(TEntity entity) => dbSet.Update(entity);
}
using System.Linq.Expressions;
using Domain.Entity;

namespace Domain.Interface;

public interface ISupplierService
{
    Task<IEnumerable<Supplier>> Query(Expression<Func<IQueryable<Supplier>, IQueryable<Supplier>>> query, int limit = 100);

    Task<IEnumerable<Supplier>> List(Expression<Func<Supplier, bool>> predicate, int limit = 100);

}
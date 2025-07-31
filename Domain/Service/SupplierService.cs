using System.Linq.Expressions;
using Domain.Entity;
using Domain.Interface;
using Microsoft.Extensions.Logging;

namespace Domain.Service;

public class SupplierService(IUnitOfWork unitOfWork, ILogger<SupplierService> logger) : ISupplierService
{
    public async Task<IEnumerable<Supplier>> List(Expression<Func<Supplier, bool>> predicate, int limit = 100) => await unitOfWork.SupplierRepository.GetAllAsync(predicate, limit);

    public async Task<IEnumerable<Supplier>> Query(Expression<Func<IQueryable<Supplier>, IQueryable<Supplier>>> query, int limit = 100)
        => await unitOfWork.SupplierRepository.Query(query, limit, s => s.Offers);
}
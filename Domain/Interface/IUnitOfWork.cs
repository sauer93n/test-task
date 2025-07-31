using Domain.Entity;

namespace Domain.Interface;

public interface IUnitOfWork : IDisposable
{
    IRepository<Offer> OfferRepository { get; }
    IRepository<Supplier> SupplierRepository { get; }

    Task<int> SaveChangesAsync();
}

using Domain.Entity;
using Domain.Interface;
using Infrastructure.Data;

namespace Infrastructure;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IRepository<Offer> offerRepo;
    private IRepository<Supplier> supplierRepo;

    public IRepository<Offer> OfferRepository
    {
        get
        {
            if (offerRepo == null)
                offerRepo = new Repository<Offer>(context);

            return offerRepo;
        }
    }

    public IRepository<Supplier> SupplierRepository
    {
        get
        {
            if (supplierRepo == null)
                supplierRepo = new Repository<Supplier>(context);

            return supplierRepo;
        }
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
}
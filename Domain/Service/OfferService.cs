using System.Linq.Expressions;
using Domain.Entity;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Domain.Service;

public class OfferService(IUnitOfWork unitOfWork, ILogger<OfferService> logger) : IOfferService
{
    public async Task<Offer> Create(Offer offer)
    {
        await unitOfWork.OfferRepository.AddAsync(offer);

        try
        {
            await unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        when (ex is DbUpdateException or
            DbUpdateConcurrencyException or
            OperationCanceledException)
        {
            logger.LogError(ex, ex.Message);
        }

        return offer;
    }

    public async Task<Offer> Create(string brand, string model, int supplierId)
    {
        var offer = new Offer()
        {
            Brand = brand,
            Model = model,
            RegistrationDate = DateTime.UtcNow,
            SupplierId = supplierId
        };

        return await Create(offer);
    }

    public Task<IEnumerable<Offer>> List(Expression<Func<Offer, bool>> predicate, int limit = 100)
    {
        return unitOfWork.OfferRepository.GetAllAsync(predicate, limit);
    }
}
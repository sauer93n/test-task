using System.Linq.Expressions;
using Domain.Entity;

namespace Domain.Interface;

public interface IOfferService
{
    Task<Offer> Create(Offer offer);

    Task<Offer> Create(string brand, string model, int supplierId);

    Task<IEnumerable<Offer>> List(Expression<Func<Offer, bool>> predicate, int limit = 100);
}
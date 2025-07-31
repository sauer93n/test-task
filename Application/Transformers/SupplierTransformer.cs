using Application.Interface;
using Application.Model.DTO;
using Domain.Entity;

namespace Application.Transformers;

public class SupplierTransformer : ITransformer<Supplier, SupplierDTO>
{
    public async Task<Supplier> Transform(SupplierDTO data) =>
        new Supplier()
        {
            CreatedAt = data.CreatedAt,
            Name = data.Name
        };

    public async Task<SupplierDTO> Transform(Supplier data) =>
        new SupplierDTO()
        {
            CreatedAt = data.CreatedAt,
            Id = data.Id,
            Name = data.Name,
            OfferIds = data.Offers.Select(o => o.Id).ToList()
        };
}
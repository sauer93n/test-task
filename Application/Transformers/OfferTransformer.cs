using Application.Interface;
using Application.Model.DTO;
using Domain.Entity;

namespace Application.Transformers;

public class OfferTransformer : ITransformer<Offer, OfferDTO>
{
    public async Task<Offer> Transform(OfferDTO data) =>
        new Offer()
        {
            Brand = data.Brand,
            Model = data.Model,
            RegistrationDate = data.RegisterAt ?? DateTime.UtcNow,
            SupplierId = data.SupplierId ?? throw new NullReferenceException(nameof(data.SupplierId))
        };

    public async Task<OfferDTO> Transform(Offer data) =>
        new OfferDTO()
        {
            Brand = data.Brand,
            Id = data.Id,
            Model = data.Model,
            RegisterAt = data.RegistrationDate,
            SupplierId = data.SupplierId
        };
}
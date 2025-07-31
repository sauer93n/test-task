using Application.Interface;
using Application.Model.DTO;
using Application.Transformers;
using Domain.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class AddApplicationServiceExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ITransformer<Offer, OfferDTO>, OfferTransformer>();
        services.AddTransient<ITransformer<Supplier, SupplierDTO>, SupplierTransformer>();

        return services;
    }
}
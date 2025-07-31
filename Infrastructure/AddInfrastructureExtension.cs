using Domain.Entity;
using Domain.Interface;
using Domain.Service;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class AddInfrastructureExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TestTask");
        services.AddDbContext<AppDbContext>((provider, options) =>
        {
            options.UseSqlServer(connectionString);
            options.UseSeeding((context, _) =>
            {
                for (int i = 0; i < 5; i++)
                {
                    context.Set<Supplier>().Add(new()
                    {
                        CreatedAt = DateTime.UtcNow,
                        Name = Guid.NewGuid().ToString()
                    });
                }
                context.SaveChanges();

                var supplierIds = context.Set<Supplier>()
                    .OrderBy(x => Guid.NewGuid())
                    .Take(5)
                    .Select(s => s.Id)
                    .ToList();

                for (int i = 0; i < 15; i++)
                {
                    context.Set<Offer>().Add(new()
                    {
                        Brand = Guid.NewGuid().ToString(),
                        Model = Guid.NewGuid().ToString(),
                        RegistrationDate = DateTime.UtcNow.AddDays(Random.Shared.Next(1, 10)),
                        SupplierId = supplierIds[Random.Shared.Next(0, supplierIds.Count - 1)]
                    });
                }
                context.SaveChanges();
            });
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IRepository<Offer>, Repository<Offer>>();
        services.AddScoped<IRepository<Supplier>, Repository<Supplier>>();
        services.AddScoped<IOfferService, OfferService>();
        services.AddScoped<ISupplierService, SupplierService>();


        return services;
    }
}
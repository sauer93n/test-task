using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class OfferConfiguration : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.Model).IsRequired();
        builder.Property(p => p.Brand).IsRequired();
        builder.Property(p => p.RegistrationDate).IsRequired();
        builder.Property(p => p.SupplierId).IsRequired();
    }
}
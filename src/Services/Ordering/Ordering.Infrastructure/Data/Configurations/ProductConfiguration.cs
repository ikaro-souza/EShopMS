using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Id).HasConversion(
            modelId => modelId.Value,
            dbId => ProductId.Of(dbId));

        builder.Property(product => product.Name).IsRequired().HasMaxLength(100);
    }
}
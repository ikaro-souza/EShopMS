using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Id).HasConversion(
            modelId => modelId.Value,
            dbId => OrderItemId.Of(dbId));
        builder.Property(entity => entity.Quantity).IsRequired();
        builder.Property(entity => entity.Price).IsRequired();

        builder.HasOne<Product>().WithMany().HasForeignKey(orderItem => orderItem.ProductId);
    }
}
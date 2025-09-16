using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Id).HasConversion(
            modelId => modelId.Value,
            dbId => CustomerId.Of(dbId));

        builder.Property(customer => customer.Name).IsRequired().HasMaxLength(100);
        builder.Property(customer => customer.Email).IsRequired().HasMaxLength(255);
        builder.HasIndex(customer => customer.Email).IsUnique();
    }
}
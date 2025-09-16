using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.Id).HasConversion(
            modelId => modelId.Value,
            dbId => OrderId.Of(dbId));

        builder.ComplexProperty(
            order => order.OrderName, nameBuilder =>
            {
                nameBuilder.Property(orderName => orderName.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .HasMaxLength(100)
                    .IsRequired();
            });

        builder.ComplexProperty(
            o => o.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();
                addressBuilder.Property(a => a.LastName)
                    .HasMaxLength(50)
                    .IsRequired();
                addressBuilder.Property(a => a.Email)
                    .HasMaxLength(50);
                addressBuilder.Property(a => a.AddressLine)
                    .HasMaxLength(180)
                    .IsRequired();
                addressBuilder.Property(a => a.Country)
                    .HasMaxLength(50);
                addressBuilder.Property(a => a.State)
                    .HasMaxLength(50);
                addressBuilder.Property(a => a.ZipCode)
                    .HasMaxLength(5)
                    .IsRequired();
            });

        builder.ComplexProperty(
            o => o.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(a => a.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(a => a.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                addressBuilder.Property(a => a.Email)
                    .HasMaxLength(50);

                addressBuilder.Property(a => a.AddressLine)
                    .HasMaxLength(180)
                    .IsRequired();

                addressBuilder.Property(a => a.Country)
                    .HasMaxLength(50);

                addressBuilder.Property(a => a.State)
                    .HasMaxLength(50);

                addressBuilder.Property(a => a.ZipCode)
                    .HasMaxLength(5)
                    .IsRequired();
            });

        builder.ComplexProperty(
            o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardHolder)
                    .HasMaxLength(50);

                paymentBuilder.Property(p => p.CardNumber)
                    .HasMaxLength(24)
                    .IsRequired();

                paymentBuilder.Property(p => p.ExpirationDte)
                    .HasMaxLength(10);

                paymentBuilder.Property(p => p.Cvv)
                    .HasMaxLength(3);

                paymentBuilder.Property(p => p.PaymentMethod);
            });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Created)
            .HasConversion(
                s => s.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(order => order.CustomerId)
            .IsRequired();
        builder.HasMany<OrderItem>().WithOne().HasForeignKey(orderItem => orderItem.OrderId);
    }
}
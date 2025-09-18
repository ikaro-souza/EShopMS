namespace Ordering.Application.DTOs;

public record OrderDto(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    OrderStatus Status,
    List<OrderItemDto> OrderItems
)
{
    public static OrderDto From(Order order)
    {
        return new OrderDto(order.Id.Value,
            order.CustomerId.Value,
            order.OrderName.Value,
            AddressDto.From(order.ShippingAddress),
            AddressDto.From(order.BillingAddress),
            PaymentDto.From(order.Payment),
            order.Status,
            order.OrderItems.Select(OrderItemDto.From).ToList()
        );
    }
}
namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.Order);

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }

    private static Order CreateNewOrder(OrderDto orderDto)
    {
        var shippingAddress = orderDto.ShippingAddress.ToAddressValueObject();
        var billingAddress = orderDto.BillingAddress.ToAddressValueObject();
        var payment = orderDto.Payment.ToPaymentValueObject();

        var order = Order.Create(
            CustomerId.Of(orderDto.CustomerId),
            OrderName.Of(orderDto.OrderName),
            shippingAddress,
            billingAddress,
            payment
        );

        foreach (var orderItem in orderDto.OrderItems)
            order.AddOrderItem(
                ProductId.Of(orderItem.ProductId),
                orderItem.Quantity,
                orderItem.Price);

        return order;
    }
}
namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.Order.Id);
        var order = await dbContext.Orders.FindAsync([orderId], cancellationToken);

        if (order is null)
            throw new OrderNotFoundException(command.Order.Id);

        var updatedShippingAddress = command.Order.ShippingAddress.ToAddressValueObject();
        var updatedBillingAddress = command.Order.BillingAddress.ToAddressValueObject();
        var updatedPayment = command.Order.Payment.ToPaymentValueObject();

        order.Update(OrderName.Of(command.Order.OrderName), updatedShippingAddress, updatedBillingAddress,
            updatedPayment, command.Order.Status);

        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
    }
}
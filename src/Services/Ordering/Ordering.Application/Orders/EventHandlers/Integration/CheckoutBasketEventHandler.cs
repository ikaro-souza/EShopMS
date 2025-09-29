using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.Application.Orders.EventHandlers.Integration;

public class CheckoutBasketEventHandler(
    ISender sender,
    ILogger<CheckoutBasketEventHandler> logger) : IConsumer<CheckoutBasketEvent>
{
    public async Task Consume(ConsumeContext<CheckoutBasketEvent> context)
    {
        logger.LogInformation("Integration event handled: {integrationEvent}", context.Message.GetType().Name);

        var command = MapCreateOrderCommand(context.Message);
        await sender.Send(command);
    }

    private CreateOrderCommand MapCreateOrderCommand(CheckoutBasketEvent message)
    {
        // Create full order with incoming event data
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.Email, message.AddressLine,
            message.Country, message.State, message.ZipCode);
        var paymentDto = new PaymentDto(message.CardHolder, message.CardNumber, message.ExpirationDate, message.Cvv,
            message.PaymentMethod);
        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
            orderId,
            message.CustomerId,
            message.UserName,
            addressDto,
            addressDto,
            paymentDto,
            OrderStatus.Pending,
            [
                new OrderItemDto(orderId, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 2, 500),
                new OrderItemDto(orderId, new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), 1, 400)
            ]);

        return new CreateOrderCommand(orderDto);
    }
}
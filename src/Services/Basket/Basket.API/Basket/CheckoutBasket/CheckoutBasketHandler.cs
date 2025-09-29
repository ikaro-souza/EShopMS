using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(CheckoutBasketDto CheckoutBasket) : ICommand<CheckoutBasketResult>;

public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.CheckoutBasket).NotNull().WithMessage("Checkout basket is required");
        RuleFor(x => x.CheckoutBasket.UserName).NotEmpty().WithMessage("User name is required");
    }
}

public class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint)
    : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(command.CheckoutBasket.UserName, cancellationToken);
        if (basket is null) throw new BasketNotFoundException(command.CheckoutBasket.UserName);

        var eventMessage = command.CheckoutBasket.Adapt<CheckoutBasketEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await repository.DeleteBasket(basket.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}
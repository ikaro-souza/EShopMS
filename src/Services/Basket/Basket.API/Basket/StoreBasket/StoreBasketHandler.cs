using Discount.GRPC;

namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string UserName);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart is required");
        RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
        RuleFor(x => x.Cart.Items).NotNull().WithMessage("Items are required");
    }
}

public class StoreBasketCommandHandler(
    IBasketRepository repository,
    DiscountProtoService.DiscountProtoServiceClient discountGrpcService)
    : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        await ApplyDiscounts(command.Cart, cancellationToken);

        await repository.StoreBasket(command.Cart, cancellationToken);
        return new StoreBasketResult(command.Cart.UserName);
    }

    private async Task ApplyDiscounts(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var cartItem in cart.Items)
        {
            var discount = await discountGrpcService.GetDiscountAsync(
                new GetDiscountRequest { ProductName = cartItem.ProductName }, cancellationToken: cancellationToken);
            cartItem.Price -= discount?.Amount ?? 0;
        }
    }
}
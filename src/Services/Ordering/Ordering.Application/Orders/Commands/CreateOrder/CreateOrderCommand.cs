namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid OrderId);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(o => o.Order.OrderName).NotEmpty().WithMessage("Order name is required");
        RuleFor(o => o.Order.CustomerId).NotNull().WithMessage("Customer is required");
        RuleFor(o => o.Order.OrderItems).NotEmpty().WithMessage("Order items should not be empty");
    }
}
namespace Ordering.Application.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto Order) : ICommand<UpdateOrderResult>, ICommand<Unit>;

public record UpdateOrderResult(bool IsSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Order.Id).NotEmpty().WithMessage("Order Id is required.");
        RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Order Name is required.");
        RuleFor(x => x.Order.CustomerId).NotNull().WithMessage("Customer Id is required.");
    }
}
namespace Ordering.Domain.Models;

public class Order : Aggregate<OrderId>
{
    private readonly List<OrderItem> _orderItems = [];
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

    public CustomerId CustomerId { get; private set; }
    public OrderName OrderName { get; private set; }
    public Address ShippingAddress { get; private set; }
    public Address BillingAddress { get; private set; }
    public Payment Payment { get; private set; }
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    public decimal TotalPrice
    {
        get => OrderItems.Sum(item => item.Price * item.Quantity);
        private set { }
    }

    public static Order Create(CustomerId customerId, OrderName orderName, Address shippingAddress,
        Address billingAddress, Payment payment)
    {
        var order = new Order
        {
            Id = OrderId.Of(Guid.NewGuid()),
            CustomerId = customerId,
            OrderName = orderName,
            ShippingAddress = shippingAddress,
            BillingAddress = billingAddress,
            Payment = payment,
            Status = OrderStatus.Pending
        };

        // TODO: Add domain event
        order.AddDomainEvent(new OrderCreatedDomainEvent(order));

        return order;
    }

    public void Update(OrderName orderName, Address shippingAddress,
        Address billingAddress, Payment payment, OrderStatus status)
    {
        OrderName = orderName;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        Payment = payment;
        Status = status;

        // TODO: Add domain event
        AddDomainEvent(new OrderUpdatedDomainEvent(this));
    }

    public void AddOrderItem(ProductId productId, int quantity, decimal price)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var orderItem = new OrderItem(Id, productId, quantity, price);
        _orderItems.Add(orderItem);
    }

    public void RemoveOrderItem(ProductId productId)
    {
        var orderItem = _orderItems.FirstOrDefault(item => item.ProductId == productId);
        if (orderItem is not null) _orderItems.Remove(orderItem);
    }
}
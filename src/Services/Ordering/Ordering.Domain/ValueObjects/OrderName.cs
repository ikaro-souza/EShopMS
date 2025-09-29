namespace Ordering.Domain.ValueObjects;

public record OrderName
{
    private const int DefaultLength = 5;

    private OrderName()
    {
    }

    private OrderName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static OrderName Of(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, DefaultLength);

        return new OrderName(value);
    }
}
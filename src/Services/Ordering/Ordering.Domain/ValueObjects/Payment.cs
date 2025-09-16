namespace Ordering.Domain.ValueObjects;

public record Payment
{
    public string? CardHolder { get; }
    public string CardNumber { get; }
    public string ExpirationDte { get; }
    public string CVV { get; }
    public int PaymentMethod { get; }
}
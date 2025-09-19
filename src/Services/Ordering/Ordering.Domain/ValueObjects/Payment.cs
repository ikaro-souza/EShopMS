namespace Ordering.Domain.ValueObjects;

public record Payment
{
    protected Payment()
    {
    }

    private Payment(string cardHolder, string cardNumber, string expirationDate, string cvv, int paymentMethod)
    {
        CardHolder = cardHolder;
        CardNumber = cardNumber;
        ExpirationDate = expirationDate;
        Cvv = cvv;
        PaymentMethod = paymentMethod;
    }

    public string? CardHolder { get; }
    public string CardNumber { get; }
    public string ExpirationDate { get; }
    public string Cvv { get; }
    public int PaymentMethod { get; }

    public static Payment Of(string cardHolder, string cardNumber, string expirationDate, string cvv, int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardHolder);
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
        ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
        ArgumentOutOfRangeException.ThrowIfNotEqual(cvv.Length, 3);

        return new Payment(cardHolder, cardNumber, expirationDate, cvv, paymentMethod);
    }
}
namespace Ordering.Application.DTOs;

public record PaymentDto(
    string CardHolder,
    string CardNumber,
    string ExpirationDate,
    string Cvv,
    int PaymentMethod
)
{
    public Payment ToPaymentValueObject()
    {
        return Payment.Of(CardHolder, CardNumber, ExpirationDate, Cvv, PaymentMethod);
    }

    public static PaymentDto From(Payment payment)
    {
        return payment.Adapt<PaymentDto>();
    }
}
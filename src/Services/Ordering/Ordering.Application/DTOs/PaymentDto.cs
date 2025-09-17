namespace Ordering.Application.DTOs;

public record PaymentDto(
    string CardHolder,
    string CardNumber,
    string ExpirationDte,
    string Cvv,
    int PaymentMethod
);
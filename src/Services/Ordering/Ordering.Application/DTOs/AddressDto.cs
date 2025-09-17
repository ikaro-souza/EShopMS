namespace Ordering.Application.DTOs;

public record AddressDto(
    string FirstName,
    string LastName,
    string Email,
    string AddressLine,
    string Country,
    string State,
    string ZipCode
)
{
    public Address ToAddressValueObject()
    {
        return Address.Of(FirstName, LastName, Email, AddressLine, Country, State, ZipCode);
    }
}
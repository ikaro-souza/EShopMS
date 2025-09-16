namespace Ordering.Domain.ValueObjects;

public record Address
{
    protected Address()
    {
    }

    private Address(string firstName, string lastName, string? email, string addressLine, string country, string state,
        string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        AddressLine = addressLine;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public string? Email { get; }
    public string AddressLine { get; }
    public string Country { get; }
    public string State { get; }
    public string ZipCode { get; }

    public static Address Of(string firstName, string lastName, string email, string addressLine,
        string country, string state, string zipCode)
    {
        ArgumentException.ThrowIfNullOrEmpty(addressLine);

        return new Address(firstName, lastName, email, addressLine, country, state, zipCode);
    }
}
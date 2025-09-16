namespace Ordering.Domain.ValueObjects;

public record Address
{
    public string FirstName { get; }
    public string LastName { get; }
    public string? Email { get; }
    public string AddressLine { get; }
    public string Country { get; }
    public string State { get; }
    public string ZipCode { get; }
}
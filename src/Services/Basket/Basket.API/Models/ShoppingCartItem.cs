namespace Basket.API.Models;

public record ShoppingCartItem
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public required string Color { get; set; }
    public decimal Price { get; set; }
    public required string ProductName { get; set; }
}
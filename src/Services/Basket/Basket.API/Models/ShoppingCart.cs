namespace Basket.API.Models;

public record ShoppingCart
{
    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    public ShoppingCart()
    {
    }

    public required string UserName { get; set; } = string.Empty;
    public required List<ShoppingCartItem> Items { get; init; } = [];
    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
}
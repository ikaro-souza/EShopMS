namespace Discount.GRPC.Models;

public class Coupon
{
    public int Id { get; set; }
    public required string ProductName { get; set; }
    public required string Description { get; set; }
    public decimal Amount { get; set; }
}
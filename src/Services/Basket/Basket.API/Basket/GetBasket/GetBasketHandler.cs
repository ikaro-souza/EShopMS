using Basket.API.Models;

namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

public record GetBasketResult(ShoppingCart ShoppingCart);

public class GetBasketHandler : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        // 1. Get from database

        return new GetBasketResult(new ShoppingCart(request.UserName));
    }
}
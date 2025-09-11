using Marten.Pagination;

namespace CatalogAPI.Products.GetProducts;

public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;

public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession documentSession)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await documentSession.Query<Product>()
            .ToPagedListAsync((int)query.PageNumber!, (int)query.PageSize!, cancellationToken);
        return new GetProductsResult(products);
    }
}
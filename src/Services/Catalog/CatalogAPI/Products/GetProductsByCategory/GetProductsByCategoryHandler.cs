namespace CatalogAPI.Products.GetProductsByCategory;

public record class GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;

public record class GetProductsByCategoryResult(IEnumerable<Product> Products);

internal class GetProductsByCategoryQueryHandler(
    IDocumentSession documentSession)
    : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query,
        CancellationToken cancellationToken)
    {
        var products = await documentSession.Query<Product>()
            .Where(product => product.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);
        return new GetProductsByCategoryResult(products);
    }
}
namespace CatalogAPI.Products.GetProductById;

public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler(IDocumentSession documentSession)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await documentSession.LoadAsync<Product>(request.ProductId, cancellationToken);
        return product is not null
            ? new GetProductByIdResult(product)
            : throw new ProductNotFoundException(request.ProductId);
    }
}
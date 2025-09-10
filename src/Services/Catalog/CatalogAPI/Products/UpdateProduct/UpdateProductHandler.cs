namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : IRequest<UpdateProductResult>;

public record UpdateProductResult(Product Product);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required.");
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
    }
}

internal class UpdateProductCommandHandler(
    IDocumentSession documentSession,
    ILogger<UpdateProductCommandHandler> logger,
    IValidator<UpdateProductCommand> validator)
    : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductCommandHandler.Handle() called");

        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        var errors = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
        if (errors.Count != 0) throw new ValidationException(errors.FirstOrDefault());

        var product = await documentSession.LoadAsync<Product>(command.Id, cancellationToken);
        if (product is null)
        {
            logger.LogWarning("Product with ID {ProductId} not found", command.Id);
            throw new ProductNotFoundException();
        }

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;
        documentSession.Update(product);

        await documentSession.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(product);
    }
}
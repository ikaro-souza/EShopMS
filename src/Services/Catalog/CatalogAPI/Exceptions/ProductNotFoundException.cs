using BuildingBlocks.Exceptions;

namespace CatalogAPI.Exceptions;

public class ProductNotFoundException(Guid id) : NotFoundException("Product", id);
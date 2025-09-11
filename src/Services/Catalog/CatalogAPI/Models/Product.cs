namespace CatalogAPI.Models;

public class Product
{
    public Product()
    {
    }

    public Product(Guid id, string name, string description, string imageFile, decimal price, List<string> category)
    {
        Id = id;
        Name = name;
        Description = description;
        ImageFile = imageFile;
        Price = price;
        Category = category;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public List<string> Category { get; set; } = [];
    public string Description { get; set; } = null!;
    public string ImageFile { get; set; } = null!;
    public decimal Price { get; set; }
}
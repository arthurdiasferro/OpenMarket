using Core.Domain.Entities;

namespace Domain.Entities.Products;

public class Product : Entity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; private set; }
    public Guid CategoryId { get; set; }
    public bool IsActive { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public void SetPrice(decimal price)
    {
        if (price < 0) throw new ArgumentException("Price cannot be negative", nameof(price));
        Price = price;
    }
}
using Core.Domain.Entities;

namespace Domain.Entities.Categories;

public class Category : Entity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
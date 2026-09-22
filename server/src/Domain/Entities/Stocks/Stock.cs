using Core.Domain.Entities;

namespace Domain.Entities.Stocks;

public class Stock : Entity
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

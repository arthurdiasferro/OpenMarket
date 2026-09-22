using Domain.Entities.Repositories;

namespace Domain.Entities.Stocks.Repository;

public interface IStockRepository : IRepository<Stock>
{
    Task<Stock?> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);
}

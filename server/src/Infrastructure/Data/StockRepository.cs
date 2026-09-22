using Domain.Entities.Stocks;
using Domain.Entities.Stocks.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class StockRepository(AppDbContext context) : PostgreRepository<Stock>(context), IStockRepository
{
    public async Task<Stock?> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(s => s.ProductId == productId, cancellationToken);
    }
}

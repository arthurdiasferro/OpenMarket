using Domain.Entities.Products;
using Domain.Entities.Products.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(AppDbContext context) : PostgreRepository<Product>(context), IProductRepository
{
    public async Task<IEnumerable<Product>> GetProductsByCategoryId(Guid categoryId)
    {
        return await DbSet
            .AsNoTracking()
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<bool> HasProductsInCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(p => p.CategoryId == categoryId, cancellationToken);
    }
}

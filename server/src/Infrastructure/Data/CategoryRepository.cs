using Domain.Entities.Categories;
using Domain.Entities.Categories.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class CategoryRepository(AppDbContext context) : PostgreRepository<Category>(context), ICategoryRepository
{
    public async Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(c => c.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.AnyAsync(c => c.Id == id, cancellationToken);
    }
}


using Domain.Entities.Repositories;

namespace Domain.Entities.Products.Repository;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsByCategoryId(Guid categoryId);

    Task<bool> HasProductsInCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
}

using Domain.Entities.Repositories;

namespace Domain.Entities.Categories.Repository;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}

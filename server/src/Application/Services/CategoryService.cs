using AutoMapper;
using Domain.Entities.Categories;
using Domain.Entities.Categories.Repository;
using Application.ViewModels.Categories;

namespace Application.Services;

public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IReadOnlyList<CategoryViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var categories = await _categoryRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IReadOnlyList<CategoryViewModel>>(categories);
    }

    public async Task<CategoryViewModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<CategoryViewModel>(category);
    }

    public async Task<CategoryViewModel?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByNameAsync(name, cancellationToken);
        return _mapper.Map<CategoryViewModel>(category);
    }

    public async Task<CategoryViewModel> AddAsync(CreateCategoryViewModel createCategoryViewModel, CancellationToken cancellationToken = default)
    {
        var category = _mapper.Map<Category>(createCategoryViewModel);

        await _categoryRepository.AddAsync(category, cancellationToken);
        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CategoryViewModel>(category);
    }
}

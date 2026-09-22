using AutoMapper;
using Core.Domain.Exceptions;
using Domain.Entities.Categories;
using Domain.Entities.Categories.Repository;
using Domain.Entities.Products.Repository;
using Application.ViewModels.Categories;

namespace Application.Services;

public class CategoryService(
    ICategoryRepository categoryRepository,
    IProductRepository productRepository,
    IMapper mapper)
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IProductRepository _productRepository = productRepository;
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

    public async Task<CategoryViewModel?> UpdateAsync(Guid id, UpdateCategoryViewModel updateCategoryViewModel, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
        if (category is null) return null;

        category.Name = updateCategoryViewModel.Name;
        category.Description = updateCategoryViewModel.Description;
        category.UpdatedAt = DateTime.UtcNow;

        _categoryRepository.Update(category);
        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CategoryViewModel>(category);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(id, cancellationToken);
        if (category is null) return false;

        if (await _productRepository.HasProductsInCategoryAsync(id, cancellationToken))
            throw new DomainException("Não é possível excluir uma categoria que possui produtos vinculados.");

        _categoryRepository.Remove(category);
        await _categoryRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}

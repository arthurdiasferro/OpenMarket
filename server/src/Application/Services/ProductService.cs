using AutoMapper;
using Core.Domain.Exceptions;
using Domain.Entities.Categories.Repository;
using Domain.Entities.Products;
using Domain.Entities.Products.Repository;
using Domain.Entities.Stocks.Repository;
using Application.ViewModels.Products;

namespace Application.Services;

public class ProductService(
    IProductRepository productRepository,
    IStockRepository stockRepository,
    ICategoryRepository categoryRepository,
    IMapper mapper)
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IReadOnlyList<ProductViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IReadOnlyList<ProductViewModel>>(products);
    }

    public async Task<ProductViewModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<ProductViewModel>(product);
    }

    public async Task<IReadOnlyList<ProductViewModel>> GetByCategoryIdAsync(Guid categoryId)
    {
        var products = await _productRepository.GetProductsByCategoryId(categoryId);
        return _mapper.Map<IReadOnlyList<ProductViewModel>>(products);
    }

    public async Task<ProductViewModel> AddAsync(CreateProductViewModel createProductViewModel, CancellationToken cancellationToken = default)
    {
        var categoryId = createProductViewModel.CategoryId ?? Guid.Empty;
        if (!await _categoryRepository.ExistsAsync(categoryId, cancellationToken))
            throw new DomainException("A categoria informada não existe.");

        var product = _mapper.Map<Product>(createProductViewModel);

        await _productRepository.AddAsync(product, cancellationToken);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductViewModel>(product);
    }

    public async Task<ProductViewModel?> UpdateAsync(Guid id, UpdateProductViewModel updateProductViewModel, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (product is null) return null;

        var categoryId = updateProductViewModel.CategoryId ?? product.CategoryId;
        if (!await _categoryRepository.ExistsAsync(categoryId, cancellationToken))
            throw new DomainException("A categoria informada não existe.");

        product.Name = updateProductViewModel.Name;
        product.Description = updateProductViewModel.Description;
        product.SetPrice(updateProductViewModel.Price ?? 0);
        product.IsActive = updateProductViewModel.IsActive ?? product.IsActive;
        product.CategoryId = categoryId;
        product.UpdatedAt = DateTime.UtcNow;

        _productRepository.Update(product);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductViewModel>(product);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (product is null) return false;

        var stock = await _stockRepository.GetByProductIdAsync(id, cancellationToken);
        if (stock is not null && stock.Quantity > 0)
            throw new DomainException("Não é possível excluir um produto com estoque. Zere o estoque antes de excluir.");

        // Remove o registro de estoque zerado associado, se existir (FK Restrict impede exclusão do produto caso contrário).
        if (stock is not null)
            _stockRepository.Remove(stock);

        _productRepository.Remove(product);
        await _productRepository.SaveChangesAsync(cancellationToken);

        return true;
    }
}

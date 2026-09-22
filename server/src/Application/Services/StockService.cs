using AutoMapper;
using Domain.Entities.Stocks;
using Domain.Entities.Stocks.Repository;
using Application.ViewModels.Stocks;

namespace Application.Services;

public class StockService(IStockRepository stockRepository, IMapper mapper)
{
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<IReadOnlyList<StockViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var stocks = await _stockRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IReadOnlyList<StockViewModel>>(stocks);
    }

    public async Task<StockViewModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var stock = await _stockRepository.GetByIdAsync(id, cancellationToken);
        return _mapper.Map<StockViewModel>(stock);
    }

    public async Task<StockViewModel?> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var stock = await _stockRepository.GetByProductIdAsync(productId, cancellationToken);
        return _mapper.Map<StockViewModel>(stock);
    }

    public async Task<StockViewModel> AddAsync(CreateStockViewModel createStockViewModel, CancellationToken cancellationToken = default)
    {
        var stock = _mapper.Map<Stock>(createStockViewModel);

        await _stockRepository.AddAsync(stock, cancellationToken);
        await _stockRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<StockViewModel>(stock);
    }
}

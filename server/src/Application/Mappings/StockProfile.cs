using AutoMapper;
using Domain.Entities.Stocks;
using Application.ViewModels.Stocks;

namespace Application.Mappings;

public class StockProfile : Profile
{
    public StockProfile()
    {
        // Entidade -> ViewModel de saída
        CreateMap<Stock, StockViewModel>();

        // ViewModel de criação -> Entidade
        // Id e CreatedAt já vêm preenchidos pelo construtor de CreateViewModel
        CreateMap<CreateStockViewModel, Stock>();
    }
}

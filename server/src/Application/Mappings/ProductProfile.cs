using AutoMapper;
using Domain.Entities.Products;
using Application.ViewModels.Products;

namespace Application.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // Entidade -> ViewModel de saída
        CreateMap<Product, ProductViewModel>();

        // ViewModel de criação -> Entidade
        // Id e CreatedAt já vêm preenchidos pelo construtor de CreateViewModel.
        // Price possui setter privado (SetPrice valida valor negativo), por isso é
        // ignorado no mapeamento automático e definido manualmente via AfterMap.
        CreateMap<CreateProductViewModel, Product>()
            .ForMember(dest => dest.Price, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive ?? true))
            .AfterMap((src, dest) => dest.SetPrice(src.Price ?? 0));
    }
}

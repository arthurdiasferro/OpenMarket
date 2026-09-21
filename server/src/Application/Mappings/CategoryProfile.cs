using AutoMapper;
using Domain.Entities.Categories;
using Application.ViewModels.Categories;

namespace Application.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        // Entidade -> ViewModel de saída
        CreateMap<Category, CategoryViewModel>();

        // ViewModel de criação -> Entidade
        // Id e CreatedAt já vêm preenchidos pelo construtor de CreateViewModel
        CreateMap<CreateCategoryViewModel, Category>();
    }
}

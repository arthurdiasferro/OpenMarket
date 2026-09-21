using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.ViewModels.Categories;

public class CreateCategoryViewModel : CreateViewModel
{
    [Required(ErrorMessage = "O nome da categoria é obrigatório")]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
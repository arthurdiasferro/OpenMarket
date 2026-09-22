using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Application.Validations;

namespace Application.ViewModels.Products;

public class CreateProductViewModel : CreateViewModel
{
    [Required(ErrorMessage = "O nome do produto é obrigatório")]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "O preço do produto é obrigatório")]
    [JsonPropertyName("price")]
    public decimal? Price { get; set; }

    [JsonPropertyName("is_active")]
    public bool? IsActive { get; set; }

    [Required(ErrorMessage = "A categoria do produto é obrigatória")]
    [NotEmptyGuid(ErrorMessage = "A categoria do produto é inválida")]
    [JsonPropertyName("category_id")]
    public Guid? CategoryId { get; set; }
}
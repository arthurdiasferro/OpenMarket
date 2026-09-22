using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Application.Validations;

namespace Application.ViewModels.Stocks;

public class CreateStockViewModel : CreateViewModel
{
    [Required(ErrorMessage = "O produto do estoque é obrigatório")]
    [NotEmptyGuid(ErrorMessage = "O produto do estoque é inválido")]
    [JsonPropertyName("product_id")]
    public Guid? ProductId { get; set; }

    [Required(ErrorMessage = "A quantidade do estoque é obrigatória")]
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }
}

using System.Text.Json.Serialization;

namespace Application.ViewModels.Stocks;

public class StockViewModel : ViewModel
{
    [JsonPropertyName("product_id")]
    public Guid ProductId { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}

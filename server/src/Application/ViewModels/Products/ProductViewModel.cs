namespace Application.ViewModels.Products;

using System.Text.Json.Serialization;
public class ProductViewModel : ViewModel
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    [JsonPropertyName("price")]
    public decimal? Price { get; set; }
    [JsonPropertyName("is_active")]
    public bool? IsActive { get; set; }
    [JsonPropertyName("category_id")]
    public Guid? CategoryId { get; set; }
    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
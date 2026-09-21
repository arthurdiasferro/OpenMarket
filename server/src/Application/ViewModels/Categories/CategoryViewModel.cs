using System.Text.Json.Serialization;

namespace Application.ViewModels.Categories
{
    public class CategoryViewModel : ViewModel
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
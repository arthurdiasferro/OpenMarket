using System.Text.Json.Serialization;

namespace Application.ViewModels;

public class ViewModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
}
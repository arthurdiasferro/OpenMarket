using System.Text.Json.Serialization;

namespace Application.ViewModels;

public abstract class CreateViewModel
{
    protected CreateViewModel()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
}
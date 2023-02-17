using System.Text.Json.Serialization;

namespace BookLibrary.Api.Models;

public abstract class ApiBaseModel
{
    [JsonPropertyName("id")]
    public string? Id { get; set; } = string.Empty;

    public ApiBaseModel(string id)
    { 
        Id = id; 
    }

    public ApiBaseModel()
    {
        Id = Guid.NewGuid().ToString();
    }
}

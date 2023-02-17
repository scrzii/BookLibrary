using BookLibrary.Storage.Models;
using System.Text.Json.Serialization;

namespace BookLibrary.Api.Models;

public class ApiGenre : ApiBaseModel
{
    [JsonPropertyName("name")] 
    public string Name { get; set; } = string.Empty;

    public static ApiGenre FromStorage(Genre genre)
    {
        return new ApiGenre
        {
            Id = genre.Id,
            Name = genre.Name
        };
    }
}

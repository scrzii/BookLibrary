using BookLibrary.Storage.Models;
using System.Text.Json.Serialization;

namespace BookLibrary.Api.Models;

public class ApiBook : ApiBaseModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("publishingYear")]
    public int PublishingYear { get; set; }
    [JsonPropertyName("genreId")]
    public string GenreId { get; set; } = string.Empty;
    [JsonPropertyName("authorIds")]
    public IEnumerable<string>? AuthorIds { get; set; } = null;
    [JsonPropertyName("redaction")]
    public string Redaction { get; set; } = string.Empty;

    public static ApiBook FromStorage(Book book, IEnumerable<string> authorIds)
    {
        return new ApiBook
        {
            Id = book.Id,
            Name = book.Name,
            PublishingYear = book.PublishingYear,
            GenreId = book.GenreId,
            AuthorIds = authorIds,
            Redaction = book.Redaction
        };
    }
}

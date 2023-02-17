using BookLibrary.Storage.Models;
using System.Text.Json.Serialization;

namespace BookLibrary.Api.Models;

public class ApiAuthor : ApiBaseModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("birthDate")]
    public DateTime BirthDate { get; set; }
    [JsonPropertyName("bookIds")]
    public IEnumerable<string>? BookIds { get; set; } = null;

    public static ApiAuthor FromStorage(Author author, IEnumerable<string> bookIds)
    {
        return new ApiAuthor
        {
            Id = author.Id,
            Name = author.Name,
            BirthDate = author.BirthDate,
            BookIds = bookIds
        };
    }
}

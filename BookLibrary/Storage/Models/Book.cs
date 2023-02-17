using BookLibrary.Api.Models;

namespace BookLibrary.Storage.Models;

public class Book : BaseModel
{
    public string Name { get; set; } = string.Empty;
    public int PublishingYear { get; set; } = 0;
    public string GenreId { get; set; } = string.Empty;
    public string Redaction { get; set; } = string.Empty; 

    public Book(ApiBook apiBook) : base(apiBook.Id!)
    {
        Name = apiBook.Name;
        PublishingYear = apiBook.PublishingYear;
        GenreId = apiBook.GenreId;
        Redaction = apiBook.Redaction;
    }
}

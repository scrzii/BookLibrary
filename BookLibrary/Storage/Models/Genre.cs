using BookLibrary.Api.Models;

namespace BookLibrary.Storage.Models;

public class Genre : BaseModel
{
    public string Name { get; set; } = string.Empty;

    public Genre(ApiGenre genre) : base(genre.Id!)
    {
        Name = genre.Name;
    }

    public Genre(string name) : base()
    {
        Name = name;
    }
}

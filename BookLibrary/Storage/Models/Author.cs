using BookLibrary.Api.Models;

namespace BookLibrary.Storage.Models;

public class Author : BaseModel
{
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }

    public Author(ApiAuthor author) : base(author.Id!)
    {
        Name = author.Name;
        BirthDate = author.BirthDate;
    }
}

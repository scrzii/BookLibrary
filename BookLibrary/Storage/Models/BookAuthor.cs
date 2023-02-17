namespace BookLibrary.Storage.Models;

public class BookAuthor : BaseModel
{
    public string BookId { get; set; }
    public string AuthorId { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        return BookId == ((BookAuthor)obj!).BookId && AuthorId == ((BookAuthor)obj!).AuthorId;
    }

    public BookAuthor(string bookId, string authorId) : base()
    {
        BookId = bookId;
        AuthorId = authorId;
    }
}

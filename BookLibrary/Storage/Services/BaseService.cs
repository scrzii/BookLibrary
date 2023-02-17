using BookLibrary.Common.Interfaces;
using BookLibrary.Storage.Models;

namespace BookLibrary.Storage.Services;

public abstract class BaseService
{
    protected IDbCollection<Book> _books;
    protected IDbCollection<Author> _authors;
    protected IDbCollection<BookAuthor> _possessions;
    protected IDbCollection<Genre> _genres;

    public BaseService()
    {
        _books = CollectionStorage.GetCollection<Book>();
        _authors = CollectionStorage.GetCollection<Author>();
        _possessions = CollectionStorage.GetCollection<BookAuthor>();
        _genres = CollectionStorage.GetCollection<Genre>();
    }
}

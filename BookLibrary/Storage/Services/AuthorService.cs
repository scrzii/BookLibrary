using BookLibrary.Common.Extensions;
using BookLibrary.Common.Interfaces;
using BookLibrary.Storage.Models;
using DocumentFormat.OpenXml.Packaging;
using System.Runtime.CompilerServices;

namespace BookLibrary.Storage.Services;

public class AuthorService : BaseService
{
    public AuthorService() : base()
    { 
    }

    public void Create(Author author)
    {
        _authors.Create(author);
    }

    public Author Read(string id)
    {
        return _authors.Read(id);
    }

    public void UpdateProfile(Author author)
    {
        _authors.Update(author);
    }

    public void UpdatePossessions(string authorId, IEnumerable<string> bookIds)
    {
        ValidateBooks(bookIds);

        var oldPossessions = _possessions.GetByCondition(x => x.AuthorId == authorId)
            .Select(x => x.Id)
            .ToHashSet();
        bookIds = bookIds.ToHashSet();

        DeleteIrrelevant(oldPossessions, bookIds);
        CreateNew(authorId, oldPossessions, bookIds);
    }

    private void ValidateBooks(IEnumerable<string> bookIds)
    {
        if (bookIds.Any(x => !_books.Exists(x)))
        {
            throw new Exception("Invalid book collection");
        }
    }

    private void DeleteIrrelevant(IEnumerable<string> old, IEnumerable<string> @new)
    {
        old.GetUnincluded(@new)
            .ToList()
            .ForEach(x => _possessions.Delete(x));
    }

    private void CreateNew(string authorId, IEnumerable<string> old, IEnumerable<string> @new)
    {
        @new.GetUnincluded(old)
            .ToList()
            .ForEach(x =>
            {
                _possessions.Create(new BookAuthor(x, authorId));
            });
    }

    public void Delete(string authorId)
    {
        DeletePossessions(authorId);
        _authors.Delete(authorId);
    }

    private void DeletePossessions(string authorId)
    {
        var toDelete = _possessions.GetByCondition(x => x.AuthorId == authorId);
        toDelete.ToList()
            .ForEach(x =>
            {
                _possessions.Delete(x.Id);
            });
    }

    public IEnumerable<Author> GetAllProfiles()
    {
        return _authors.GetAll();
    }

    public IEnumerable<Book> GetBooks(string authorId)
    {
        return GetBookIds(authorId)
            .Select(x => _books.Read(x));
    }

    public IEnumerable<string> GetBookIds(string authorId)
    {
        return _possessions.GetByCondition(x => x.AuthorId == authorId)
            .Select(x => x.BookId);
    }
}

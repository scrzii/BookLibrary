using BookLibrary.Common.Extensions;
using BookLibrary.Common.Interfaces;
using BookLibrary.Storage.Models;
using System.Runtime.InteropServices;

namespace BookLibrary.Storage.Services;

public class BookService : BaseService
{
    public BookService() : base()
    {
    }

    public void Create(Book book)
    {
        _books.Create(book);
    }

    public Book Read(string id)
    {
        return _books.Read(id);
    }

    public void UpdateInfo(Book book)
    {
        _books.Update(book);
    }

    public void UpdatePossessions(string bookId, IEnumerable<string> authorIds)
    {
        ValidateAuthors(authorIds);

        var oldPossessions = _possessions.GetByCondition(x => x.BookId == bookId)
            .Select(x => x.Id)
            .ToHashSet();
        authorIds = authorIds.ToHashSet();

        DeleteIrrelevant(oldPossessions, authorIds);
        CreateNew(bookId, oldPossessions, authorIds);
    }

    private void ValidateAuthors(IEnumerable<string> authorIds)
    {
        if (authorIds.Any(x => !_authors.Exists(x)))
        {
            throw new Exception("Invalid author collection");
        }
    }

    private void DeleteIrrelevant(IEnumerable<string> old, IEnumerable<string> @new)
    {
        old.GetUnincluded(@new)
            .ToList()
            .ForEach(x => _possessions.Delete(x));
    }

    private void CreateNew(string bookId, IEnumerable<string> old, IEnumerable<string> @new)
    {
        @new.GetUnincluded(old)
            .ToList()
            .ForEach(x =>
            {
                _possessions.Create(new BookAuthor(bookId, x));
            });
    }

    public void Delete(string bookId)
    {
        DeletePossessions(bookId);
        _authors.Delete(bookId);
    }

    private void DeletePossessions(string bookId)
    {
        var toDelete = _possessions.GetByCondition(x => x.BookId == bookId);
        toDelete.ToList()
            .ForEach(x =>
            {
                _possessions.Delete(x.Id);
            });
    }

    public IEnumerable<Book> GetAllInfos()
    {
        return _books.GetAll();
    }

    public IEnumerable<Author> GetOwners(string bookId)
    {
        return GetAuthorIds(bookId)
            .Select(x => _authors.Read(x));
    }

    public IEnumerable<string> GetAuthorIds(string bookId)
    {
        return _possessions.GetByCondition(x => x.BookId == bookId)
            .Select(x => x.AuthorId);
    }
}

using BookLibrary.Api.Models;
using BookLibrary.Storage.Models;
using BookLibrary.Storage.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Api.Controllers;

[ApiController]
[Route("[controller]", Name = "book")]
public class BookController : ControllerBase
{
    private BookService _bookService;

    public BookController()
    {
        _bookService = new BookService();
    }
 
    [HttpPost("create")]
    public void Create(ApiBook book)
    {
        _bookService.Create(new Book(book));
    }

    [HttpGet("read")]
    public ApiBook Read(string id)
    {
        return ApiBook.FromStorage(_bookService.Read(id), _bookService.GetAuthorIds(id));
    }

    [HttpGet("readAll")]
    public IEnumerable<ApiBook> ReadAll()
    {
        return _bookService.GetAllInfos()
            .Select(x => ApiBook.FromStorage(x, _bookService.GetAuthorIds(x.Id)));
    }

    [HttpPost("update")]
    public void Update(ApiBook book)
    {
        if (book.Id is null)
        {
            throw new Exception("Id not specified");
        }

        _bookService.UpdateInfo(new Book(book));
        _bookService.UpdatePossessions(book.Id!, book.AuthorIds ?? new string[0]);
    }

    [HttpGet("delete")]
    public void Delete(string id)
    {
        _bookService.Delete(id);
    }
}

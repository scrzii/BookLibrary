using BookLibrary.Api.Models;
using BookLibrary.Storage.Models;
using BookLibrary.Storage.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Api.Controllers;

[ApiController]
[Route("[controller]", Name = "author")]
public class AuthorController : ControllerBase
{
    private AuthorService _authorService;

    public AuthorController()
    {
        _authorService = new AuthorService();
    }

    [HttpPost("create")]
    public void Create(ApiAuthor author)
    {
        _authorService.Create(new Author(author));
    }

    [HttpGet("read")]
    public ApiAuthor Read(string id)
    {
        return ApiAuthor.FromStorage(_authorService.Read(id), _authorService.GetBookIds(id));
    }

    [HttpGet("readAll")]
    public IEnumerable<ApiAuthor> ReadAll()
    {
        return _authorService.GetAllProfiles()
            .Select(x => ApiAuthor.FromStorage(x, _authorService.GetBookIds(x.Id)));
    }

    [HttpPost("update")]
    public void Update(ApiAuthor author)
    {
        if (author.Id is null)
        {
            throw new Exception("Id not specified");
        }

        _authorService.UpdateProfile(new Author(author));
        _authorService.UpdatePossessions(author.Id!, author.BookIds ?? new string[0]);
    }

    [HttpGet("delete")]
    public void Delete(string id)
    {
        _authorService.Delete(id);
    }
}

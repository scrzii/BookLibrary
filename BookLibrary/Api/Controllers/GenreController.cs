using BookLibrary.Api.Models;
using BookLibrary.Storage.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Api.Controllers;

[ApiController]
[Route("[controller]", Name = "genre")]
public class GenreController : ControllerBase
{
    private GenreService _genreService;

    public GenreController()
    {
        _genreService = new GenreService();
    }

    [HttpGet("readAll")]
    public IEnumerable<ApiGenre> GetAll()
    {
        return _genreService.GetAll()
            .Select(x => ApiGenre.FromStorage(x));
    }
}

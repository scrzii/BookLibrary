using BookLibrary.Common.Interfaces;
using BookLibrary.Storage.Models;

namespace BookLibrary.Storage.Services;

public class GenreService : BaseService
{
    public GenreService() : base()
    {
    }

    public void Create(Genre genre)
    {
        _genres.Create(genre);
    }

    public void Read(string id)
    {
        _genres.Read(id);
    }

    public IEnumerable<Genre> GetAll()
    {
        return _genres.GetAll();
    }
}

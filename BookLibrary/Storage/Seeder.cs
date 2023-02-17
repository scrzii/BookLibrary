using BookLibrary.Storage.Models;
using BookLibrary.Storage.Services;

namespace BookLibrary.Storage;

public static class Seeder
{
    public static void FillGenres()
    {
        var genreService = new GenreService();

        genreService.Create(new Genre("Comedy"));
        genreService.Create(new Genre("Horror"));
        genreService.Create(new Genre("Romantic"));
    }
}

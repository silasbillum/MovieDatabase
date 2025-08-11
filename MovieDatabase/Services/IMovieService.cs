using Models;
namespace MovieDatabase.Services
{
    public interface IMovieService
    {
        Task<List<Movie>> SearchMoviesAsync(string movieId);
        Task<List<Movie>> TrendingMoviesAsync(int page);
        Task<List<Movie>> TopRatedMoviesAsync();
        Task<Movie> MoviesDetailsAsync(int movieId);
        Task<List<TVShows>> TrendingTVAsync();
    }
}

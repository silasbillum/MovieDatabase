using Models;
namespace MovieDatabase.Services
{
    public interface IMovieService
    {
        Task<List<Movie>> SearchMoviesAsync(string movieId);
        Task<List<Movie>> TrendingMoviesAsync(int page);
        Task<MovieSearchResult> TopRatedMoviesAsync(int page);
        Task<Movie> MoviesDetailsAsync(int movieId);
        Task<TVShows> TVDetailsAsync(int movieId);
        Task<List<TVShows>> TrendingTVAsync(int page);
        Task<List<Images>> GetMovieBackdropsAsync(int movieId);
        Task<List<Videos>> GetMovieVideosAsync(int movieId);
    }
}

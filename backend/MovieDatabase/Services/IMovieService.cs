using Models;

namespace MovieDatabase.Services;

public interface IMovieService
{
    Task<MovieSearchResult> GetTrendingMoviesAsync(int page = 1);
    Task<MovieSearchResult> SearchMoviesAsync(string query, int page = 1);
    Task<MovieSearchResult> GetTopRatedMoviesAsync(int page = 1);
    Task<Movie> GetMovieDetailsAsync(int movieId);
}
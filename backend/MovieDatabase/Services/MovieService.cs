using Models;
using RestSharp;
using System.Text.Json;

namespace MovieDatabase.Services;

public class MovieService : IMovieService
{
    private readonly RestClient _client;
    private readonly string _apiKey;
    private readonly string _baseUrl = "https://api.themoviedb.org/3";

    public MovieService(IConfiguration configuration)
    {
        _client = new RestClient(_baseUrl);
        _apiKey = Environment.GetEnvironmentVariable("TMDBApiKey") ?? 
                 configuration["TMDBApiKey"] ?? 
                 throw new InvalidOperationException("TMDB API Key not configured");
    }

    public async Task<MovieSearchResult> GetTrendingMoviesAsync(int page = 1)
    {
        try
        {
            var request = new RestRequest($"/trending/movie/day")
                .AddParameter("api_key", _apiKey)
                .AddParameter("page", page);

            var response = await _client.ExecuteAsync(request);
            
            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };
                
                var result = JsonSerializer.Deserialize<MovieSearchResult>(response.Content, options);
                return result ?? new MovieSearchResult { Results = new List<Movie>() };
            }

            throw new Exception($"Failed to fetch trending movies: {response.ErrorMessage}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting trending movies: {ex.Message}");
        }
    }

    public async Task<MovieSearchResult> SearchMoviesAsync(string query, int page = 1)
    {
        try
        {
            var request = new RestRequest($"/search/movie")
                .AddParameter("api_key", _apiKey)
                .AddParameter("query", query)
                .AddParameter("page", page);

            var response = await _client.ExecuteAsync(request);
            
            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };
                
                var result = JsonSerializer.Deserialize<MovieSearchResult>(response.Content, options);
                return result ?? new MovieSearchResult { Results = new List<Movie>() };
            }

            throw new Exception($"Failed to search movies: {response.ErrorMessage}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error searching movies: {ex.Message}");
        }
    }

    public async Task<MovieSearchResult> GetTopRatedMoviesAsync(int page = 1)
    {
        try
        {
            var request = new RestRequest($"/movie/top_rated")
                .AddParameter("api_key", _apiKey)
                .AddParameter("page", page);

            var response = await _client.ExecuteAsync(request);
            
            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };
                
                var result = JsonSerializer.Deserialize<MovieSearchResult>(response.Content, options);
                return result ?? new MovieSearchResult { Results = new List<Movie>() };
            }

            throw new Exception($"Failed to fetch top rated movies: {response.ErrorMessage}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting top rated movies: {ex.Message}");
        }
    }

    public async Task<Movie> GetMovieDetailsAsync(int movieId)
    {
        try
        {
            var request = new RestRequest($"/movie/{movieId}")
                .AddParameter("api_key", _apiKey)
                .AddParameter("append_to_response", "videos,images");

            var response = await _client.ExecuteAsync(request);
            
            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };
                
                var result = JsonSerializer.Deserialize<Movie>(response.Content, options);
                return result ?? throw new Exception("Movie not found");
            }

            throw new Exception($"Failed to fetch movie details: {response.ErrorMessage}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error getting movie details: {ex.Message}");
        }
    }
}
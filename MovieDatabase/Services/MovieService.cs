// Services/MovieService.cs
using Microsoft.AspNetCore.Http.HttpResults;
using Models;
using MovieDatabase.Services;
using RestSharp;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

public class MovieService : IMovieService
{
    private readonly IConfiguration _config;
    private readonly RestClient _client;

    public MovieService(IConfiguration config)
    {
        _config = config;
        _client = new RestClient("https://api.themoviedb.org/3"); // manually created
    }

   

    public async Task<List<Movie>> SearchMoviesAsync(string query)
    {
        var apiKey = _config["TMDB:ApiKey"] ?? Environment.GetEnvironmentVariable("TMDBApiKey");
        var request = new RestRequest("search/movie", Method.Get);
        request.AddParameter("api_key", apiKey);
        request.AddParameter("query", query);

        var response = await _client.ExecuteAsync(request);

        if (!response.IsSuccessful)
            throw new Exception("TMDB API request failed.");

        var result = JsonSerializer.Deserialize<MovieSearchResult>(response.Content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Console.WriteLine($"TMDB returned {result?.Results?.Count ?? 0} results");
        foreach (var m in result.Results)
        {
            Console.WriteLine($"Title: {m.MovieTitle}, PosterPath: {m.PosterPath}");
        }


        return result?.Results ?? new List<Movie>();
    }



    public async Task<List<Movie>> TrendingMoviesAsync(int page)
    {
        var apiKey = _config["TMDB:ApiKey"] ?? Environment.GetEnvironmentVariable("TMDBApiKey");
        var options = new RestClientOptions("https://api.themoviedb.org/3/discover/movie")
        {
            ThrowOnAnyError = true
        };
        var client = new RestClient(options);

        var request = new RestRequest();
        request.AddParameter("api_key", apiKey);
        request.AddParameter("include_adult", "true");
        request.AddParameter("include_video", "true");
        request.AddParameter("language", "en-US");
        request.AddParameter("page", page.ToString());
        request.AddParameter("sort_by", "popularity.desc");

        var response = await client.ExecuteAsync(request);

        if (!response.IsSuccessful)
            throw new Exception("TMDB API request failed: " + response.ErrorMessage);

        var result = JsonSerializer.Deserialize<MovieSearchResult>(response.Content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result?.Results ?? new List<Movie>();
    }

    public async Task<Movie> MoviesDetailsAsync(int movieId)
    {
        var apiKey = _config["TMDB:ApiKey"] ?? Environment.GetEnvironmentVariable("TMDBApiKey");
        var options = new RestClientOptions("https://api.themoviedb.org/3")
        {
            ThrowOnAnyError = true
        };
        var client = new RestClient(options);

        var request = new RestRequest($"/movie/{movieId}");
        request.AddParameter("api_key", apiKey);
        request.AddParameter("language", "en-US");

        var response = await client.ExecuteAsync(request);

        if (!response.IsSuccessful)
            throw new Exception("TMDB API request failed: " + response.ErrorMessage);

        var movie = JsonSerializer.Deserialize<Movie>(response.Content!,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return movie;
    }

    public async Task<List<TVShows>> TrendingTVAsync()
    {
        var apiKey = _config["TMDB:ApiKey"] ?? Environment.GetEnvironmentVariable("TMDBApiKey");
        var options = new RestClientOptions("https://api.themoviedb.org/3/discover/tv")
        {
            ThrowOnAnyError = true
        };
        var client = new RestClient(options);

        var request = new RestRequest();
        request.AddParameter("api_key", apiKey);
        request.AddParameter("include_adult", "true");
        request.AddParameter("include_null_first_air_dates", "false");
        request.AddParameter("language", "en-US");
        request.AddParameter("page", "10");
        request.AddParameter("screened_theatrically", "true");
        request.AddParameter("sort_by", "popularity.desc");

        var response = await client.ExecuteAsync(request);

        if (!response.IsSuccessful)
            throw new Exception("TMDB API request failed: " + response.ErrorMessage);

        var result = JsonSerializer.Deserialize<TVResult>(response.Content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result?.Results ?? new List<TVShows>();
    }

    public async Task<List<Movie>> TopRatedMoviesAsync()
    {
        var apiKey = _config["TMDB:ApiKey"] ?? Environment.GetEnvironmentVariable("TMDBApiKey");
        var options = new RestClientOptions("https://api.themoviedb.org/3/movie/top_rated")
        {
            ThrowOnAnyError = true
        };
        var client = new RestClient(options);

        var request = new RestRequest();
        request.AddParameter("api_key", apiKey);
        request.AddParameter("language", "en-US");
        request.AddParameter("page", "1");

        var response = await client.ExecuteAsync(request);

        if (!response.IsSuccessful)
            throw new Exception("TMDB API request failed: " + response.ErrorMessage);

        var result = JsonSerializer.Deserialize<MovieSearchResult>(response.Content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return result?.Results ?? new List<Movie>();
    }




}



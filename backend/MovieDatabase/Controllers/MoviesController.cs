using Microsoft.AspNetCore.Mvc;
using MovieDatabase.Services;
using Models;

namespace MovieDatabase.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;
    private readonly ILogger<MoviesController> _logger;

    public MoviesController(IMovieService movieService, ILogger<MoviesController> logger)
    {
        _movieService = movieService;
        _logger = logger;
    }

    /// <summary>
    /// Get trending movies
    /// </summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <returns>List of trending movies</returns>
    [HttpGet("trending")]
    public async Task<ActionResult<MovieSearchResult>> GetTrendingMovies([FromQuery] int page = 1)
    {
        try
        {
            _logger.LogInformation("Getting trending movies for page {Page}", page);
            var result = await _movieService.GetTrendingMoviesAsync(page);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting trending movies");
            return StatusCode(500, new { error = "Failed to get trending movies", details = ex.Message });
        }
    }

    /// <summary>
    /// Search movies by query
    /// </summary>
    /// <param name="query">Search term</param>
    /// <param name="page">Page number (default: 1)</param>
    /// <returns>List of movies matching the search query</returns>
    [HttpGet("search")]
    public async Task<ActionResult<MovieSearchResult>> SearchMovies([FromQuery] string query, [FromQuery] int page = 1)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest(new { error = "Query parameter is required" });
            }

            _logger.LogInformation("Searching movies with query '{Query}' for page {Page}", query, page);
            var result = await _movieService.SearchMoviesAsync(query, page);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching movies with query '{Query}'", query);
            return StatusCode(500, new { error = "Failed to search movies", details = ex.Message });
        }
    }

    /// <summary>
    /// Get top-rated movies
    /// </summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <returns>List of top-rated movies</returns>
    [HttpGet("top-rated")]
    public async Task<ActionResult<MovieSearchResult>> GetTopRatedMovies([FromQuery] int page = 1)
    {
        try
        {
            _logger.LogInformation("Getting top-rated movies for page {Page}", page);
            var result = await _movieService.GetTopRatedMoviesAsync(page);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting top-rated movies");
            return StatusCode(500, new { error = "Failed to get top-rated movies", details = ex.Message });
        }
    }

    /// <summary>
    /// Get movie details by ID
    /// </summary>
    /// <param name="id">Movie ID</param>
    /// <returns>Detailed movie information</returns>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Movie>> GetMovieDetails(int id)
    {
        try
        {
            _logger.LogInformation("Getting movie details for ID {MovieId}", id);
            var movie = await _movieService.GetMovieDetailsAsync(id);
            return Ok(movie);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting movie details for ID {MovieId}", id);
            
            if (ex.Message.Contains("Movie not found"))
            {
                return NotFound(new { error = "Movie not found", movieId = id });
            }
            
            return StatusCode(500, new { error = "Failed to get movie details", details = ex.Message });
        }
    }
}
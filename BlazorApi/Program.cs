using Microsoft.EntityFrameworkCore;
using MovieDatabase;
using MovieDatabase.Components;
using MovieDatabase.Services;


var builder = WebApplication.CreateBuilder(args);

// Konfigurer port 8510
builder.WebHost.UseUrls("http://+:8510");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Læs miljøvariabler fra Dokploy .env
var connectionString = Environment.GetEnvironmentVariable("DefaultConnection")
    ?? builder.Configuration.GetConnectionString("DefaultConnection");

var tmdbApiKey = Environment.GetEnvironmentVariable("TMDBApiKey")
    ?? builder.Configuration["TMDB:ApiKey"];

Console.WriteLine("connectionString: " + (string.IsNullOrEmpty(connectionString) ? "IKKE FUNDET" : "FUNDET"));
Console.WriteLine("TMDBApiKey: " + (string.IsNullOrEmpty(tmdbApiKey) ? "IKKE FUNDET" : "FUNDET"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add CORS for Flutter app
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IMovieService, MovieService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add API endpoints for Flutter app
app.MapGet("/api/movies/trending", async (IMovieService movieService, int page = 1) =>
{
    try
    {
        var movies = await movieService.TrendingMoviesAsync(page);
        return Results.Ok(new { results = movies });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error getting trending movies: {ex.Message}");
    }
});

app.MapGet("/api/movies/search", async (IMovieService movieService, string query) =>
{
    try
    {
        if (string.IsNullOrWhiteSpace(query))
            return Results.BadRequest("Query parameter is required");

        var movies = await movieService.SearchMoviesAsync(query);
        return Results.Ok(new { results = movies });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error searching movies: {ex.Message}");
    }
});

app.MapGet("/api/movies/top-rated", async (IMovieService movieService, int page = 1) =>
{
    try
    {
        var movies = await movieService.TopRatedMoviesAsync(page);
        return Results.Ok(new { results = movies.Results });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error getting top rated movies: {ex.Message}");
    }
});

app.MapGet("/api/movies/{id:int}", async (IMovieService movieService, int id) =>
{
    try
    {
        var movie = await movieService.MoviesDetailsAsync(id);
        return Results.Ok(movie);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error getting movie details: {ex.Message}");
    }
});

app.Run();

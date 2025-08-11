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

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

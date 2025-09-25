# Movie Database Backend

ASP.NET Core 8 Web API backend for the Movie Database application.

## 🚀 Quick Start

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (for PostgreSQL database)
- TMDB API Key ([Get one free here](https://www.themoviedb.org/settings/api))

### Setup Instructions

1. **Get TMDB API Key**
   - Go to [TMDB API](https://www.themoviedb.org/settings/api)
   - Create a free account and get your API key

2. **Set up Environment Variable**
   ```powershell
   # Windows PowerShell
   $env:TMDBApiKey="your_actual_api_key_here"
   
   # Or alternatively, edit appsettings.json and replace YOUR_API_KEY_HERE
   ```

3. **Start Database** (from backend directory)
   ```bash
   docker-compose up -d
   ```

4. **Run the Backend** (from backend/MovieDatabase directory)
   ```bash
   dotnet restore
   dotnet run
   ```

5. **Verify API is Working**
   - Open browser to `http://localhost:8510/swagger`
   - Test the trending movies endpoint: `http://localhost:8510/api/movies/trending`

## 🔧 API Endpoints

| Method | Endpoint | Description | Parameters |
|--------|----------|-------------|------------|
| GET | `/api/movies/trending` | Get trending movies | `?page=1` |
| GET | `/api/movies/search` | Search movies | `?query=movie_name&page=1` |
| GET | `/api/movies/top-rated` | Get top-rated movies | `?page=1` |
| GET | `/api/movies/{id}` | Get movie details | `{id}` - Movie ID |

## 🛠️ Development

### Project Structure
```
MovieDatabase/
├── Controllers/          # API Controllers
│   └── MoviesController.cs
├── Data/                # Entity Framework
│   └── AppDbContext.cs
├── Services/            # Business Logic
│   ├── IMovieService.cs
│   └── MovieService.cs
├── Program.cs           # App configuration
└── appsettings.json     # Configuration
```

### Technologies Used
- **ASP.NET Core 8** - Web API framework
- **Entity Framework Core** - ORM for PostgreSQL
- **RestSharp** - HTTP client for TMDB API
- **Swagger/OpenAPI** - API documentation
- **Docker** - Database containerization

## 🔐 Configuration

### Database Connection
The application uses PostgreSQL. Update connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=moviedb;Username=postgres;Password=password"
  }
}
```

### TMDB API Key
Set your TMDB API key via:
1. Environment variable: `TMDBApiKey`
2. Or in `appsettings.json`: Replace `YOUR_API_KEY_HERE`

## 🐳 Docker Support

### Database Only (Recommended for Development)
```bash
docker-compose up -d
```

### Full Application
```bash
docker build -t movie-database-backend .
docker run -p 8510:8510 -e TMDBApiKey="your_key" movie-database-backend
```

## 🚀 Production Deployment

1. **Build for Production**
   ```bash
   dotnet publish -c Release -o publish
   ```

2. **Set Environment Variables**
   - `TMDBApiKey` - Your TMDB API key
   - `ASPNETCORE_ENVIRONMENT` - Set to `Production`
   - Connection strings for your production database

3. **Run**
   ```bash
   dotnet MovieDatabase.dll
   ```

## 🧪 Testing

### Manual Testing with Swagger
1. Start the application: `dotnet run`
2. Open `http://localhost:8510/swagger`
3. Test endpoints directly in the browser

### Example API Calls
```bash
# Get trending movies
curl http://localhost:8510/api/movies/trending

# Search for movies
curl "http://localhost:8510/api/movies/search?query=avengers"

# Get movie details
curl http://localhost:8510/api/movies/299536
```

## ⚡ Performance Notes

- **CORS**: Configured to allow requests from Flutter frontend
- **Caching**: Consider adding response caching for production
- **Rate Limiting**: TMDB has rate limits - consider caching responses
- **Database**: Uses Entity Framework with PostgreSQL for scalability

## 🔧 Troubleshooting

### Common Issues

1. **"TMDB API Key not configured"**
   - Set the `TMDBApiKey` environment variable
   - Or update `appsettings.json` with your API key

2. **Database Connection Errors**
   - Ensure PostgreSQL is running: `docker-compose ps`
   - Check connection string in `appsettings.json`

3. **Port Already in Use**
   - The app runs on port 8510 by default
   - Change in `launchSettings.json` if needed

4. **CORS Errors from Frontend**
   - Ensure CORS policy includes your frontend URLs
   - Check `Program.cs` CORS configuration

### Logs
Check console output for detailed error messages and API call logs.
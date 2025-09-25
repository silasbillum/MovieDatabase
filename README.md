# Movie Database

A full-stack movie application with C# ASP.NET Core backend and Flutter frontend.

## 🏗️ Architecture

This is a monorepo containing both frontend and backend:

- **Backend**: C# ASP.NET Core Web API with Blazor Server
- **Frontend**: Flutter mobile/desktop application
- **Database**: PostgreSQL (via Docker)
- **External API**: The Movie Database (TMDB) API

## 📁 Project Structure

```
MovieDatabase/
├── backend/                 # C# ASP.NET Core backend
│   ├── MovieDatabase/       # Main web application
│   ├── Models/             # Shared models
│   ├── docker-compose.yml # Database setup
│   └── Dockerfile         # Backend containerization
├── frontend/               # Flutter frontend
│   └── moviedatabase/     # Flutter app
├── docs/                  # Documentation
└── README.md             # This file
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Flutter SDK](https://flutter.dev/docs/get-started/install)
- [Docker](https://www.docker.com/get-started) (for database)
- TMDB API Key ([Get one here](https://www.themoviedb.org/settings/api))

### Backend Setup

1. Navigate to the backend directory:
   ```bash
   cd backend
   ```

2. Set up your TMDB API key:
   ```bash
   # Set environment variable
   $env:TMDBApiKey="your_api_key_here"  # Windows PowerShell
   export TMDBApiKey="your_api_key_here"  # Linux/Mac
   ```

3. Start the database:
   ```bash
   docker-compose up -d
   ```

4. Run the backend:
   ```bash
   cd MovieDatabase
   dotnet run
   ```

   The API will be available at `http://localhost:8510`

### Frontend Setup

1. Navigate to the frontend directory:
   ```bash
   cd frontend/moviedatabase
   ```

2. Get Flutter dependencies:
   ```bash
   flutter pub get
   ```

3. Run the Flutter app:
   ```bash
   flutter run -d windows  # For Windows
   flutter run -d chrome   # For Web
   ```

## 🔧 API Endpoints

The backend provides the following REST API endpoints:

- `GET /api/movies/trending?page={page}` - Get trending movies
- `GET /api/movies/search?query={query}` - Search movies
- `GET /api/movies/top-rated?page={page}` - Get top-rated movies
- `GET /api/movies/{id}` - Get movie details

## 📱 Features

### Backend Features
- TMDB API integration
- Movie search and discovery
- Trending and top-rated movies
- RESTful API design
- CORS support for frontend
- PostgreSQL database integration

### Frontend Features
- Netflix-style UI design
- Real-time movie search
- Trending movies carousel
- Touch-optimized navigation
- Cross-platform support (Windows, Web, Mobile)
- Pull-to-refresh functionality

## 🛠️ Tech Stack

### Backend
- **Framework**: ASP.NET Core 8
- **Language**: C#
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core
- **API Client**: RestSharp
- **Containerization**: Docker

### Frontend
- **Framework**: Flutter
- **Language**: Dart
- **HTTP Client**: http package
- **State Management**: StatefulWidget
- **UI Design**: Material Design with custom Netflix-style theming

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙏 Acknowledgments

- [The Movie Database (TMDB)](https://www.themoviedb.org/) for providing the movie data API
- Flutter team for the amazing cross-platform framework
- .NET team for the robust backend framework
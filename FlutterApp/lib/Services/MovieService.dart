import "dart:convert";
import "package:http/http.dart" as http;
import "../Models/Movie.dart";
import "../Models/MovieSearchResult.dart";

class MovieService {
  // C# Blazor API base URL
  static const String baseUrl = 'http://localhost:8510/api';

  // Search movies
  static Future<List<Movie>> searchMovies(String query) async {
    try {
      final response = await http.get(
        Uri.parse('$baseUrl/movies/search?query=${Uri.encodeComponent(query)}'),
        headers: {'Content-Type': 'application/json'},
      );

      if (response.statusCode == 200) {
        final Map<String, dynamic> data = json.decode(response.body);
        final searchResult = MovieSearchResult.fromJson(data);
        return searchResult.results;
      } else {
        throw Exception('Failed to search movies: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('Error searching movies: $e');
    }
  }

  // Get trending movies
  static Future<List<Movie>> getTrendingMovies({int page = 1}) async {
    try {
      print(
          'Fetching trending movies from: $baseUrl/movies/trending?page=$page');

      final response = await http.get(
        Uri.parse('$baseUrl/movies/trending?page=$page'),
        headers: {'Content-Type': 'application/json'},
      ).timeout(const Duration(seconds: 10));

      print('Response status: ${response.statusCode}');
      print('Response body: ${response.body}');

      if (response.statusCode == 200) {
        final Map<String, dynamic> data = json.decode(response.body);
        final searchResult = MovieSearchResult.fromJson(data);
        return searchResult.results;
      } else {
        throw Exception(
            'Failed to get trending movies. Status: ${response.statusCode}, Body: ${response.body}');
      }
    } catch (e) {
      print('Error details: $e');
      if (e.toString().contains('SocketException') ||
          e.toString().contains('Connection refused')) {
        throw Exception(
            'Cannot connect to server. Make sure your backend is running on $baseUrl');
      } else if (e.toString().contains('TimeoutException')) {
        throw Exception(
            'Request timeout. Server may be slow or not responding.');
      } else {
        throw Exception('Error getting trending movies: $e');
      }
    }
  }

  // Get top rated movies
  static Future<List<Movie>> getTopRatedMovies({int page = 1}) async {
    try {
      final response = await http.get(
        Uri.parse('$baseUrl/movies/top-rated?page=$page'),
        headers: {'Content-Type': 'application/json'},
      );

      if (response.statusCode == 200) {
        final Map<String, dynamic> data = json.decode(response.body);
        final searchResult = MovieSearchResult.fromJson(data);
        return searchResult.results;
      } else {
        throw Exception(
            'Failed to get top rated movies: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('Error getting top rated movies: $e');
    }
  }

  // Get movie details
  static Future<Movie> getMovieDetails(int movieId) async {
    try {
      final response = await http.get(
        Uri.parse('$baseUrl/movies/$movieId'),
        headers: {'Content-Type': 'application/json'},
      );

      if (response.statusCode == 200) {
        final Map<String, dynamic> data = json.decode(response.body);
        return Movie.fromJson(data);
      } else {
        throw Exception('Failed to get movie details: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('Error getting movie details: $e');
    }
  }
}

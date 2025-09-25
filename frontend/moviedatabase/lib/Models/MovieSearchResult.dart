import 'Movie.dart';

class MovieSearchResult {
  final int page;
  final List<Movie> results;
  final int totalResults;
  final int totalPages;

  MovieSearchResult({
    required this.page,
    required this.results,
    required this.totalResults,
    required this.totalPages,
  });

  factory MovieSearchResult.fromJson(Map<String, dynamic> json) {
    var list = json['results'] as List? ?? [];
    List<Movie> movieList = list.map((i) => Movie.fromJson(i)).toList();

    return MovieSearchResult(
      page: json['page'] ?? 1,
      results: movieList,
      totalResults: json['total_results'] ?? json['totalResults'] ?? 0,
      totalPages: json['total_pages'] ?? json['totalPages'] ?? 1,
    );
  }
}

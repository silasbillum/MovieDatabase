class Movie {
  final int id;
  final String title;
  final String overview;
  final String posterPath;
  final String releaseDate;
  final double voteAverage;
  final String originalLanguage;
  final int? budget;
  final int? revenue;
  final int? runtime;

  Movie({
    required this.id,
    required this.title,
    required this.overview,
    required this.posterPath,
    required this.releaseDate,
    this.voteAverage = 0.0,
    this.originalLanguage = '',
    this.budget,
    this.revenue,
    this.runtime,
  });

  factory Movie.fromJson(Map<String, dynamic> json) {
    return Movie(
      id: json['id'] ?? 0,
      title: json['title'] ?? json['movieTitle'] ?? 'Unknown Title',
      overview: json['overview'] ?? 'No overview available',
      posterPath: json['poster_path'] ?? json['posterPath'] ?? '',
      releaseDate: json['release_date'] ?? json['releaseDate'] ?? '',
      voteAverage: (json['vote_average'] ?? json['rating'] ?? 0.0).toDouble(),
      originalLanguage: json['original_language'] ?? json['language'] ?? '',
      budget: json['budget'],
      revenue: json['revenue'],
      runtime: json['runtime'] ?? json['runTime'],
    );
  }

  String get fullPosterUrl {
    if (posterPath.isEmpty) return '';
    return posterPath.startsWith('http')
        ? posterPath
        : 'https://image.tmdb.org/t/p/w500$posterPath';
  }
}

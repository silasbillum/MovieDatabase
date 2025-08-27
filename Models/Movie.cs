using System.ComponentModel.DataAnnotations;

namespace Models
{
    using System.Text.Json.Serialization;

    public class Movie
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string MovieTitle { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("original_language")]
        public string Language { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("vote_average")]
        public double Rating { get; set; }

        [JsonPropertyName("budget")]
        public int Budget { get; set; }
        [JsonPropertyName("revenue")]
        public int Revenue { get; set; }

        [JsonPropertyName("runtime")]
        public int RunTime { get; set; }

        [JsonPropertyName("Genres")]
        public List<Genre> Genres { get; set; } = new List<Genre>();

        [JsonPropertyName("production_companies")]
        public List<Production> ProductionCompanies { get; set; } = new List<Production>();

        [JsonPropertyName("backdrops")]
        public List<Images> Backdrops { get; set; } = new List<Images>();

        [JsonPropertyName("videos")]
        public List<Videos> Videos { get; set; } = new List<Videos>();

        public string FullPosterUrl =>
            !string.IsNullOrWhiteSpace(PosterPath)
                ? $"https://image.tmdb.org/t/p/w500{PosterPath}"
                : "/images/placeholder.png";
    }

}
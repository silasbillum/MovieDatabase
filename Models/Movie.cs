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

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("vote_average")]
        public double? Rating { get; set; }

        public string FullPosterUrl =>
            !string.IsNullOrWhiteSpace(PosterPath)
                ? $"https://image.tmdb.org/t/p/w500{PosterPath}"
                : "/images/placeholder.png";
    }

}

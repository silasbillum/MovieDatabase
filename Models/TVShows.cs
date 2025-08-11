using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class TVShows
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string MovieTitle { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("first_air_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("vote_average")]
        public double? Rating { get; set; }

        [JsonPropertyName("original_language")]
        public string? Language { get; set; }


        public string FullPosterUrl =>
            !string.IsNullOrWhiteSpace(PosterPath)
                ? $"https://image.tmdb.org/t/p/w500{PosterPath}"
                : "/images/placeholder.png";
    }
}


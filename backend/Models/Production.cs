using System.Text.Json.Serialization;
namespace Models
{
    public class Production
    {
        [JsonPropertyName("id")]
        public int Id { get; set; } // Primary Key

        [JsonPropertyName("logo_path")]
        public string LogoPath { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("original_country")]
        public string OriginalCountry { get; set; }

        public string FullLogoUrl =>
           !string.IsNullOrWhiteSpace(LogoPath)
               ? $"https://image.tmdb.org/t/p/w500{LogoPath}"
               : "/images/placeholder.png";
    }
}
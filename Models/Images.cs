using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class Images
    {
        [JsonPropertyName("aspect_ratio")]
        public double AspectRatio { get; set; }
        [JsonPropertyName("height")]
        public int Height { get; set; }
        [JsonPropertyName("width")]
        public int Width { get; set; }
        [JsonPropertyName("file_path")]
        public string FilePath { get; set; }


        public string FullPosterUrl =>
           !string.IsNullOrWhiteSpace(FilePath)
               ? $"https://image.tmdb.org/t/p/w500{FilePath}"
               : "/images/placeholder.png";
    }

}

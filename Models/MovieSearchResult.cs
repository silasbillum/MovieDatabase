using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class MovieSearchResult
    {
        [JsonPropertyName("results")]
        public List<Movie> Results { get; set; }
        [JsonPropertyName("page")]
        public int Page { get; set; }
        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
    }
}

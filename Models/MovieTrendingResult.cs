using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class MovieTrendingResult
    {
        [JsonPropertyName("results")]
        public List<Movie> Results { get; set; }
    }
}

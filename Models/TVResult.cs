using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models
{
    public class TVResult
    {
        [JsonPropertyName("results")]
        public List<TVShows> Results { get; set; }
    }
}

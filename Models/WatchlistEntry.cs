using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WatchlistEntry
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public string UserId { get; set; }
        public DateTime AddedAt { get; set; }
    }
}

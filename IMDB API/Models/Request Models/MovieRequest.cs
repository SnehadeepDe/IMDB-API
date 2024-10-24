using System.Collections.Generic;

namespace IMDB_API.Models.Request_Models
{
    public class MovieRequest
    {
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        public string CoverImage { get; set; }
        public int ProducerId { get; set; }
        public string ActorIds { get; set; } 
        public string GenreIds { get; set; } 
    }

}

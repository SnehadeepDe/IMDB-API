using IMDB_API.Models;
using System.Collections.Generic;

namespace IMDB_API.Models.Response_Models
{
    public class MovieResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }
        public string CoverImage { get; set; }
        public ProducerResponse Producer { get; set; }
        public List<ActorResponse> Actors { get; set; } = new List<ActorResponse>();
        public List<GenreResponse> Genres { get; set; } = new List<GenreResponse>();
        public List<ReviewResponse> Reviews { get; set; } = new List<ReviewResponse>();
    }
}

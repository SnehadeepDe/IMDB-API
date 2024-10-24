using System;

namespace IMDB_API.Models.Request_Models
{
    public class ActorRequest
    {
        public string Name { get; set; }
        public string Bio { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
    }
}

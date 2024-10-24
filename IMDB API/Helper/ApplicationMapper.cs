using AutoMapper;
using IMDB_API.Models.Request_Models;
using IMDB_API.Models.Response_Models;
using IMDB_API.Models;

namespace IMDB_API.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<MovieRequest, Movie>();
            CreateMap<Movie, MovieResponse>();

            CreateMap<ActorRequest, Actor>();
            CreateMap<Actor, ActorResponse>();

            CreateMap<GenreRequest, Genre>();
            CreateMap<Genre, GenreResponse>();

            CreateMap<ProducerRequest, Producer>();
            CreateMap<Producer, ProducerResponse>();

            CreateMap<ReviewRequest, Review>();
            CreateMap<Review, ReviewResponse>();
        }
    }
}

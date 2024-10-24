using AutoMapper;
using Firebase.Storage;
using IMDB_API.CustomException;
using IMDB_API.Models;
using IMDB_API.Models.Request_Models;
using IMDB_API.Models.Response_Models;
using IMDB_API.Repository.Interfaces;
using IMDB_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB_API.Services
{

    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorService _actorService;
        private readonly IGenreService _genreService;
        private readonly IProducerService _producerService;
        private readonly IReviewService _reviewService;
        private readonly string _firebaseStorageBucket;
        private readonly IMapper _mapper;
        public MovieService(IMovieRepository movieRepository, IActorService actorService, IGenreService genreService, IProducerService producerService, IReviewService reviewService, IMapper mapper, IOptions<FirebaseStorageSetting> firebaseStorageSetting)
        {
            _movieRepository = movieRepository;
            _actorService = actorService;
            _genreService = genreService;
            _producerService = producerService;
            _reviewService = reviewService;
            _firebaseStorageBucket = firebaseStorageSetting.Value.Bucket;
            _mapper = mapper;
        }

        public List<MovieResponse> GetAll(Filter filter)
        {
            var movies = _movieRepository.GetAll();
            if (filter.Year.HasValue)
            {
                movies = movies.Where(m => m.YearOfRelease == filter.Year.Value).ToList();
            }
            var movieResponses = _mapper.Map<List<MovieResponse>>(movies)
                .Select(movieResponse =>
                {
                    var movie = movies.First(m => m.Id == movieResponse.Id);

                    movieResponse.Producer = _producerService.GetById(movie.ProducerId);

                    movieResponse.Actors = _actorService.GetAll(movie.Id).ToList();

                    movieResponse.Genres = _genreService.GetAll(movie.Id).ToList();

                    movieResponse.Reviews = _reviewService.GetAll(movie.Id).ToList();

                    return movieResponse;
                });

            return movieResponses.ToList();
        }
        public MovieResponse GetById(int id)
        {
            var movie = _movieRepository.GetById(id);
            if (movie == null)
            {
                throw new NotFoundException("Movie not found");
            }
            var movieResponse = _mapper.Map<MovieResponse>(movie);

            movieResponse.Producer = _producerService.GetById(movie.ProducerId);

            movieResponse.Actors = _actorService.GetAll(movie.Id).ToList();

            movieResponse.Genres = _genreService.GetAll(movie.Id).ToList();

            movieResponse.Reviews = _reviewService.GetAll(movie.Id).ToList();

            return movieResponse;
        }
        public int Create(MovieRequest movieRequest)
        {
            var validationError = Validate(movieRequest);
            if (!string.IsNullOrEmpty(validationError))
            {
                throw new BadRequestException(validationError);
            }
            string actorIds = movieRequest.ActorIds;
            string genreIds = movieRequest.GenreIds;
            var movie = _mapper.Map<Movie>(movieRequest);
            return _movieRepository.Create(movie, actorIds, genreIds);
        }
        public void Update(int id, MovieRequest movieRequest)
        {
            GetById(id);
            var validationError = Validate(movieRequest);
            if (!string.IsNullOrEmpty(validationError))
            {
                throw new BadRequestException(validationError);
            }

            var movie = _mapper.Map<Movie>(movieRequest);
            movie.Id = id;
            string actorIds = movieRequest.ActorIds;
            string genreIds = movieRequest.GenreIds;
            _movieRepository.Update(id, movie, actorIds, genreIds);
        }

        public void Delete(int id)
        {
            GetById(id);
            _movieRepository.Delete(id);
        }
        public async Task UploadImage(IFormFile file)
        {
            string directoryName = "movie-posters";

            if (file == null || file.Length == 0)
                throw new BadRequestException("No file uploaded.");
            
            var firebaseStorage = new FirebaseStorage(_firebaseStorageBucket);

            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var fileExtension = Path.GetExtension(file.FileName);
            var guidValue = Guid.NewGuid().ToString();
            
            var newFileName = $"{fileName}({guidValue}){fileExtension}";
            
           await firebaseStorage
                .Child(directoryName)
                .Child(newFileName)
                .PutAsync(file.OpenReadStream());
        }
        private string Validate(MovieRequest movieRequest)
        {
            if (string.IsNullOrEmpty(movieRequest.Name))
                return "Movie name is required.";

            if (movieRequest.YearOfRelease <= 1600 || movieRequest.YearOfRelease > DateTime.Now.Year)
                return "Year of release is invalid.";

            if (string.IsNullOrEmpty(movieRequest.Plot))
                return "Movie plot is required.";

            var producer = _producerService.GetById(movieRequest.ProducerId);
            if (producer == null)
                return "Producer not found.";

            if (string.IsNullOrEmpty(movieRequest.CoverImage))
                return "Cover image is required.";

            var actorIds = movieRequest.ActorIds.Split(',');
            foreach (var id in actorIds)
            {
                var actor = _actorService.GetById(int.Parse(id));
                if (actor == null)
                    return $"Actor with ID {int.Parse(id)} not found.";
            }

            var genreIds = movieRequest.GenreIds.Split(',');
            foreach (var id in genreIds)
            {
                var genre = _genreService.GetById(int.Parse(id));
                if (genre == null)
                    return $"Genre with ID {int.Parse(id)} not found.";
            }
            return null;
        }
    }
}

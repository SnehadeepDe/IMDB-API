using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using IMDB_API.Repository.Interfaces;
using IMDB_API.Services.Interfaces;
using IMDB_API.Models.Response_Models;
using IMDB_API.CustomException;
using IMDB_API.Models.Request_Models;
using IMDB_API.Models;
using AutoMapper;

namespace IMDB_API.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }
        public List<GenreResponse> GetAll(int? movieId)
        {
            var genres = _genreRepository.GetAll(movieId);
            return _mapper.Map<List<GenreResponse>>(genres);
        }
        public GenreResponse GetById(int id)
        {
            var genre = _genreRepository.GetById(id);
            if (genre == null)
            {
                throw new NotFoundException("Genre not found");
            }
            return _mapper.Map<GenreResponse>(genre);
        }
        public int Create(GenreRequest genreRequest)
        {
            var validationError = Validate(genreRequest);

            if (!string.IsNullOrEmpty(validationError))
            {
                throw new BadRequestException(validationError);
            }

            var genre = _mapper.Map<Genre>(genreRequest);
            return _genreRepository.Create(genre);
        }
        public void Update(int id, GenreRequest genreRequest)
        {
            GetById(id);
            var validationError = Validate(genreRequest);
            if (!string.IsNullOrEmpty(validationError))
            {
                throw new BadRequestException(validationError);
            }

            var genre = _mapper.Map<Genre>(genreRequest);
            genre.Id = id;
            _genreRepository.Update(id, genre);
        }
        public void Delete(int id)
        {
            GetById(id);
            _genreRepository.Delete(id);
        }
        private string Validate(GenreRequest genreRequest)
        {
            if (string.IsNullOrEmpty(genreRequest.Name))
                return "Genre name is required.";

            return null;
        }
    }
}
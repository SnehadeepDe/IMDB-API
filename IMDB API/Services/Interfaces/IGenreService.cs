using IMDB_API.Models;
using IMDB_API.Models.Request_Models;
using IMDB_API.Models.Response_Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_API.Services.Interfaces
{
    public interface IGenreService
    {
        List<GenreResponse> GetAll(int? movieId);
        GenreResponse GetById(int id);
        int Create(GenreRequest genreRequest);
        void Update(int id, GenreRequest genreRequest);
        void Delete(int id);
    }
}

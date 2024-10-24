using IMDB_API.Models;
using IMDB_API.Models.Request_Models;
using IMDB_API.Models.Response_Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_API.Services.Interfaces
{
    public interface IMovieService
    {
        List<MovieResponse> GetAll(Filter filter);
        MovieResponse GetById(int id);
        int Create(MovieRequest movieRequest);
        void Update(int id, MovieRequest movieRequest);
        void Delete(int id);
        Task UploadImage(IFormFile file);
    }
}

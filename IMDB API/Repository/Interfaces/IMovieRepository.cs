using IMDB_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_API.Repository.Interfaces
{
    public interface IMovieRepository
    {
        List<Movie> GetAll();
        Movie GetById(int id);
        int Create(Movie movie, string actorIds, string genreIds);
        void Update(int id, Movie movie, string actorIds, string genreIds);
        void Delete(int id);
    }
}
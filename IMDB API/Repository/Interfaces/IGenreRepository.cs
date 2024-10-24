using IMDB_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_API.Repository.Interfaces
{
    public interface IGenreRepository
    {
        List<Genre> GetAll(int? movieId);
        Genre GetById(int id);
        int Create(Genre genre);
        void Update(int id, Genre genre);
        void Delete(int id);
    }
}
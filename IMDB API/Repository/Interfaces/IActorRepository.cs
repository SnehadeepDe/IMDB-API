using IMDB_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_API.Repository.Interfaces
{
    public interface IActorRepository
    {
        List<Actor> GetAll(int? movieId);
        Actor GetById(int id);
        int Create(Actor actor);
        void Update(int id, Actor actor);
        void Delete(int id);
    }
}

using IMDB_API.Models.Request_Models;
using IMDB_API.Models.Response_Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_API.Services.Interfaces
{
    public interface IActorService
    {
        List<ActorResponse> GetAll(int? movieId);
        ActorResponse GetById(int id);
        int Create(ActorRequest actorRequest);
        void Update(int id, ActorRequest actorRequest);
        void Delete(int id);
    }
}

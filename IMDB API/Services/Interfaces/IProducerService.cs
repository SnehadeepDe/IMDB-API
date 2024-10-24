using IMDB_API.Models;
using IMDB_API.Models.Request_Models;
using IMDB_API.Models.Response_Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_API.Services.Interfaces
{
    public interface IProducerService
    {
        List<ProducerResponse> GetAll();
        ProducerResponse GetById(int id);
        int Create(ProducerRequest producerRequest);
        void Update(int id, ProducerRequest producerRequest);
        void Delete(int id);
    }
}

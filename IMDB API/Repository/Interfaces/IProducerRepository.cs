using IMDB_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_API.Repository.Interfaces
{
    public interface IProducerRepository
    {
        List<Producer> GetAll();
        Producer GetById(int id);
        int Create(Producer producer);
        void Update(int id, Producer producer);
        void Delete(int id);
    }
}
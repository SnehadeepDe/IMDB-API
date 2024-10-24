using IMDB_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_API.Repository.Interfaces
{
    public interface IReviewRepository
    {
        List<Review> GetAll(int movieId);
        Review GetById(int id);
        int Create(Review review);
        void Update(int id, Review review);
        void Delete(int id);
    }
}
using IMDB_API.Models;
using IMDB_API.Models.Request_Models;
using IMDB_API.Models.Response_Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB_API.Services.Interfaces
{
    public interface IReviewService
    {
        List<ReviewResponse> GetAll(int movieId);
        ReviewResponse GetById(int movieId, int id);
        int Create(ReviewRequest reviewRequest, int movieId);
        void Update(int id, ReviewRequest reviewRequest);
        void Delete(int reviewId);
    }
}

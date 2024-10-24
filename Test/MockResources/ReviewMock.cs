using IMDB_API.Models;
using IMDB_API.Repository.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDB_API.test.MockResources
{
    public class ReviewMock
    {
        public static readonly Mock<IReviewRepository> ReviewRepoMock = new Mock<IReviewRepository>();
        public static readonly List<Review> _reviews = new List<Review>
        {
            new Review
            {
               Id = 1,
               Message = "Review 1",
               MovieId = 1
            },
            new Review
            {
                Id = 2,
                Message = "Review 2",
                MovieId = 2
            }
        };
        public static void MockGetAll()
        {
            ReviewRepoMock.Setup(x => x.GetAll(It.IsAny<int>()))
                .Returns((int movieId) => _reviews.Where(r => r.MovieId == movieId ).ToList());
        }
        public static void MockGetById()
        {
            ReviewRepoMock.Setup(x => x.GetById(It.IsAny<int>()))
                         .Returns((int reviewId) => _reviews.FirstOrDefault(r => r.Id == reviewId));
        }

        public static void MockCreate()
        {
            ReviewRepoMock.Setup(x => x.Create(It.IsAny<Review>()))
                         .Returns(_reviews.Last().Id + 1);
        }
        public static void MockUpdate()
        {
            ReviewRepoMock.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Review>()));

        }
        public static void MockDelete()
        {
            ReviewRepoMock.Setup(x => x.Delete(It.IsAny<int>()));
        }
    }
}

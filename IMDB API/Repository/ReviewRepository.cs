using IMDB_API;
using IMDB_API.Repository;
using IMDB_API.Repository.Interfaces;
using Microsoft.Extensions.Options;
using IMDB_API.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace IMDB_API.Repository
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.IMDBDB)
        {
        }
        public List<Review> GetAll(int movieId)
        {
            const string query = @"
SELECT [Id]
      ,[MovieId]
      ,[Message]
  FROM [Foundation].[Reviews](NOLOCK)
WHERE [MovieId] = @Id";
            return base.GetAll(query, movieId);
        }
        public Review GetById(int id)
        {
            const string query = @"
SELECT [Id]
      ,[MovieId]
      ,[Message]
  FROM [Foundation].[Reviews] (NOLOCK)
  WHERE [ID] = @Id ";
            return base.GetById(query, id);
        }
        public int Create(Review review)
        {
            const string query = @"
INSERT INTO [Foundation].[Reviews] (MovieId, Message)
VALUES (@movieId, @Message)
SELECT CAST(SCOPE_IDENTITY() as int)";
            return base.Create(query, review);
        }
        public void Update(int id, Review review)
        {
            const string query = @"
UPDATE [Foundation].[Reviews]
SET [MovieId] = @MovieId, 
[Message] = @Message
WHERE [Id] = @Id";

            var parameters = new
            {
                Id = id,
                review.MovieId,
                review.Message,
            };
            base.Update(query, parameters);
        }
        public void Delete(int id)
        {
            const string query = @"
DELETE FROM [Foundation].[Reviews]
WHERE [Id] = @Id";
            base.Delete(query, id);
        }
    }
}
using IMDB_API.Repository;
using IMDB_API;
using Microsoft.Extensions.Options;
using IMDB_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMDB_API.Repository.Interfaces;

namespace IMDB_API.Repository
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.IMDBDB)
        {
        }
        public List<Genre> GetAll(int? movieId)
        {
            string query;
            if (movieId.HasValue)
            {
                query = @"SELECT
g.Id,
g.Name
FROM Foundation.Genres g
INNER JOIN Foundation.MovieGenres mg ON g.Id = mg.GenreId
WHERE mg.MovieId = @Id; ";
                
            }
            else
            {
                query = @"
SELECT [Id], [Name]
FROM [Foundation].[Genres] (NOLOCK)";

            }
            return base.GetAll(query, movieId??null);
        }

        public Genre GetById(int id)
        {
            const string query = @"
SELECT [Id], [Name]
FROM [Foundation].[Genres] (NOLOCK)
WHERE [ID] = @Id";
            return base.GetById(query, id);
        }

        public int Create(Genre genre)
        {
            const string query = @"
INSERT INTO [Foundation].[Genres] (Name)
VALUES (@Name)
SELECT CAST(SCOPE_IDENTITY() as int)";
            return base.Create(query, genre);
        }

        public void Update(int id, Genre genre)
        {
            const string query = @"
UPDATE [Foundation].[Genres]
SET [Name] = @Name
WHERE [Id] = @Id";

            var parameters = new
            {
                Id = id,
                genre.Name
            };
            base.Update(query, parameters);
        }

        public void Delete(int id)
        {
            const string query = @"
DELETE FROM [Foundation].[Genres] WHERE [Id] = @Id;
DELETE FROM [Foundation].[MovieGenres] WHERE [GenreId] = @Id;";
            base.Delete(query, id);
        }
    }
}
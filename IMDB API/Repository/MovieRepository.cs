using IMDB_API;
using IMDB_API.Repository;
using Microsoft.Extensions.Options;
using IMDB_API.Models;
using IMDB_API.Repository.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace IMDB_API.Repository
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.IMDBDB)
        {
        }

        public List<Movie> GetAll()
        {
            const string query = @"SELECT 
                Id,
                Name,
                YearOfRelease,
                Plot,
                CoverImage,
                ProducerId
                FROM Foundation.Movies;";
            return base.GetAll(query,null);
        }

        public Movie GetById(int id)
        {
            const string query = @"SELECT 
                Id,
                Name,
                YearOfRelease,
                Plot,
                CoverImage,
                ProducerId
                FROM Foundation.Movies
                WHERE Id = @Id;";
            return base.GetById(query, id);
        }

        public int Create(Movie movie, string actorIds, string genreIds)
        {
            const string query = "usp_AddMovie";
            var parameters = new
            {
                movie.Name,
                movie.YearOfRelease,
                movie.Plot,
                movie.CoverImage,
                movie.ProducerId,
                ActorIds = actorIds,
                GenreIds = genreIds
            };
            return base.Create(query, parameters, commandType: CommandType.StoredProcedure);
        }

        public void Update(int id, Movie movie, string actorIds, string genreIds)
        {
            const string query = "usp_UpdateMovie";
            var parameters = new
            {
                Id = id,
                movie.Name,
                movie.YearOfRelease,
                movie.Plot,
                movie.CoverImage,
                movie.ProducerId,
                ActorIds = actorIds,
                GenreIds = genreIds
            };
            base.Update(query, parameters, commandType: CommandType.StoredProcedure);
        }

        public void Delete(int id)
        {
            const string query = @"
                DELETE FROM [Foundation].[MovieActors] WHERE [MovieId] = @Id;
                DELETE FROM [Foundation].[MovieGenres] WHERE [MovieId] = @Id;
                DELETE FROM [Foundation].[Reviews] WHERE [MovieId] = @Id;   
                DELETE FROM [Foundation].[Movies] WHERE [Id] = @Id;";
            base.Delete(query, id);
        }
    }
}
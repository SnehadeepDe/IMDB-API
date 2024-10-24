using IMDB_API.Models;
using IMDB_API.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace IMDB_API.Repository
{
    public class ActorRepository : BaseRepository<Actor>, IActorRepository
    {
        public ActorRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.IMDBDB)
        {
        }

        public int Create(Actor actor)
        {
            const string query = @"
INSERT INTO [Foundation].[Actors] (Name, Gender, DOB, Bio)
VALUES (@Name, @Gender, @DOB, @Bio)
SELECT CAST(SCOPE_IDENTITY() as int)";
            return base.Create(query, actor);
        }

        public void Delete(int id)
        {
            const string query = @"
DELETE FROM [Foundation].[Actors] WHERE [Id] = @Id;
DELETE FROM [Foundation].[MovieActors] WHERE [ActorId] = @Id;";
            base.Delete(query, id);
        }

        public Actor GetById(int id)
        {
            const string query = @"
SELECT [Id], [Name], [Gender], [DOB], [Bio]
FROM [Foundation].[Actors] (NOLOCK)
WHERE [ID] = @Id";
            return base.GetById(query, id);
        }

        public List<Actor> GetAll(int? movieId)
        {
            string query;
            if (movieId.HasValue)
            {
                query = @"SELECT
a.Id,
a.Name,
a.Bio,
a.DOB,
a.Gender
FROM Foundation.Actors a
INNER JOIN Foundation.MovieActors ma ON a.Id = ma.ActorId
WHERE ma.MovieId = @Id; ";
            }
            else{
                query = @"
SELECT [Id], [Name], [Gender], [DOB], [Bio]
FROM [Foundation].[Actors] (NOLOCK)";
               
            }
            return base.GetAll(query, movieId??null);
        }
        public void Update(int id, Actor actor)
        {
            const string query = @"
UPDATE [Foundation].[Actors]
SET [Name] = @Name, 
[Gender] = @Gender, 
[DOB] = @DOB, 
[Bio] = @Bio
WHERE [Id] = @Id";

            var parameters = new
            {
                Id = id,
                actor.Name,
                actor.Gender,
                actor.DOB,
                actor.Bio
            };
            base.Update(query, parameters);
        }
    }
}
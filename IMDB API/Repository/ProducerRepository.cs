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
    public class ProducerRepository : BaseRepository<Producer>, IProducerRepository
    {
        public ProducerRepository(IOptions<ConnectionString> connectionString)
            : base(connectionString.Value.IMDBDB)
        {
        }
        public List<Producer> GetAll()
        {
            const string query = @"
SELECT [Id]
      ,[Name]
      ,[Gender]
      ,[DOB]
      ,[Bio]
  FROM [Foundation].[Producers] (NOLOCK)";
            return base.GetAll(query,null);
        }
        public Producer GetById(int id)
        {
            const string query = @"
SELECT [Id]
      ,[Name]
      ,[Gender]
      ,[DOB]
      ,[Bio]
  FROM [Foundation].[Producers] (NOLOCK)
  WHERE [ID] = @Id";
            return base.GetById(query, id);
        }
        public int Create(Producer producer)
        {
            const string query = @"
INSERT INTO [Foundation].[Producers] (Name, Gender, DOB, Bio)
VALUES (@Name, @Gender, @DOB, @Bio)
SELECT CAST(SCOPE_IDENTITY() as int)";
            return base.Create(query, producer);
        }
        public void Update(int id, Producer producer)
        {
            const string query = @"
UPDATE [Foundation].[Producers]
SET [Name] = @Name, 
[Gender] = @Gender, 
[DOB] = @DOB, 
[Bio] = @Bio
WHERE [Id] = @Id";

            var parameters = new
            {
                Id = id,
                producer.Name,
                producer.Gender,
                producer.DOB,
                producer.Bio
            };
            base.Update(query, parameters);
        }
        public void Delete(int id)
        {
            const string query = @"
DELETE FROM [Foundation].[Producers] WHERE [Id] = @Id;
DELETE FROM [Foundation].[Movies] WHERE [ProducerId] = @Id;";
            base.Delete(query, id);
        }
    }
}
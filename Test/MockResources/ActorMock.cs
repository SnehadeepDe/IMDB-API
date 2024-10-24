using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMDB_API.Models;
using IMDB_API.Repository.Interfaces;
using Moq;

namespace IMDB_API.test.MockResources
{
    public class ActorMock
    {
        /// <summary>
        /// See how we are using Moq - https://github.com/moq/moq4
        /// </summary>
        public static readonly Mock<IActorRepository> ActorRepoMock = new Mock<IActorRepository>();

        private static readonly List<Actor> _actors = new List<Actor>
        {
            new Actor
            {
                Id = 1,
                Name = "Actor 1",
                Bio = "Bio 1",
                DOB = new DateTime(2000,5,5),
                Gender = "Male"
            },
            new Actor
            {
                Id = 2,
                Name = "Actor 2",
                Bio = "Bio 2",
                DOB = new DateTime(2000,5,5),
                Gender = "Female"
            }
        };

       public static Dictionary<int, List<int>> movieActors = new Dictionary<int, List<int>>
       {
           { 1, new List<int> { _actors[0].Id, _actors[1].Id } },
            {2, new List<int> { _actors[1].Id } }
       };
        public static void MockGetAll()
        {
            ActorRepoMock.Setup(x => x.GetAll(It.IsAny<int?>()))
                .Returns((int? movieId) =>
                {
                    if (movieId.HasValue && movieActors.TryGetValue(movieId.Value, out var actorIds))
                    {
                        return _actors.Where(a => actorIds.Contains(a.Id)).ToList();
                    }
                    return _actors;
                });
        }
        public static void MockGetById()
        {
            ActorRepoMock.Setup(x => x.GetById(It.IsAny<int>()))
                         .Returns((int actorId) => _actors.FirstOrDefault(a => a.Id == actorId));
        }

        public static void MockCreate()
        {
            ActorRepoMock.Setup(x => x.Create(It.IsAny<Actor>()))
                         .Returns(_actors.Last().Id +1 );
        }
        public static void MockUpdate()
        {
            ActorRepoMock.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Actor>()));

        }
        public static void MockDelete()
        {
            ActorRepoMock.Setup(x => x.Delete(It.IsAny<int>()));
        }
    }
}
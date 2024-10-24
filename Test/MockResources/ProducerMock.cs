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
    public class ProducerMock
    {
        public static readonly Mock<IProducerRepository> ProducerRepoMock = new Mock<IProducerRepository>();
        private static readonly List<Producer> _producers = new List<Producer>()
        {
            new Producer
            {
                Id = 1,
                Name = "Producer 1",
                Bio = "Bio 1",
                DOB = new DateTime(2000,05,05),
                Gender = "Male"
            },
            new Producer
            {
                Id = 2,
                Name = "Producer 2",
                Bio = "Bio 2",
                DOB = new DateTime(2000,05,05),
                Gender = "Female"
            }
        };

        public static void MockGetAll()
        {
            ProducerRepoMock.Setup(x => x.GetAll()).Returns(_producers);
        }
        public static void MockGetById()
        {
            ProducerRepoMock.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns((int producerId) => _producers.FirstOrDefault(p => p.Id == producerId));
        }
        public static void MockCreate()
        {
            ProducerRepoMock.Setup(x => x.Create(It.IsAny<Producer>()))
                .Returns(_producers.Last().Id + 1);
        }
        public static void MockUpdate()
        {
            ProducerRepoMock.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Producer>()));
              
        }
        public static void MockDelete()
        {
            ProducerRepoMock.Setup(x => x.Delete(It.IsAny<int>()));
        }
    }
}

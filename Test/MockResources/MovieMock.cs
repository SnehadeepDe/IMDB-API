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
    public class MovieMock
    {
            public static readonly Mock<IMovieRepository> MovieRepoMock = new Mock<IMovieRepository>();
            private static readonly List<Movie> _movies = new List<Movie>()
        {
            new Movie
            {
                Id = 1,
                Name = "Movie 1",
                Plot = "Plot 1",
                CoverImage = "image1.jpg",
                ProducerId = 1,
                YearOfRelease = 2000
            },
            new Movie
            {
                Id = 2,
                Name = "Movie 2",
                Plot = "Plot 2",
                CoverImage = "image2.jpg",
                ProducerId = 2,
                YearOfRelease = 2000
            }
        };
            public static void MockGetAll()
            {
                MovieRepoMock.Setup(x => x.GetAll()).Returns(_movies);
            }
            public static void MockGetById()
            {
                MovieRepoMock.Setup(x => x.GetById(It.IsAny<int>()))
                    .Returns((int movieId) => _movies.FirstOrDefault(m => m.Id == movieId));
            }
            public static void MockCreate()
            {
                MovieRepoMock.Setup(x => x.Create(It.IsAny<Movie>(),It.IsAny<string>(),It.IsAny<string>()))
                    .Returns(_movies.Last().Id + 1);
            }
            public static void MockUpdate()
            {
                MovieRepoMock.Setup(x => x.Update(It.IsAny<int>(),It.IsAny<Movie>(), It.IsAny<string>(), It.IsAny<string>()));

            }
            public static void MockDelete()
            {
                MovieRepoMock.Setup(x => x.Delete(It.IsAny<int>()));
            }
        }
}
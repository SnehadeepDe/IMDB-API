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
    public class GenreMock
    {
        public static readonly Mock<IGenreRepository> GenreRepoMock = new Mock<IGenreRepository>();
        private static readonly List<Genre> _genres = new List<Genre>()
        {
            new Genre
            {
                Id = 1,
                Name = "Genre 1",
            },
            new Genre
            {
                Id= 2,
                Name = "Genre 2"
            }
        };
        public static Dictionary<int, List<int>> movieGenres = new Dictionary<int, List<int>>
        {
            { 1, new List<int> { _genres[0].Id, _genres[1].Id }},
            { 2, new List<int> { _genres[1].Id }}
        };
        public static void MockGetAll()
        {
            GenreRepoMock.Setup(x => x.GetAll(It.IsAny<int?>()))
                 .Returns((int? movieId) =>
                 {
                     if (movieId.HasValue && movieGenres.TryGetValue(movieId.Value, out var genreIds))
                     {
                         return _genres.Where(a => genreIds.Contains(a.Id)).ToList();
                     }
                     return _genres;
                 });
        }
        public static void MockGetById()
        {
            GenreRepoMock.Setup(x => x.GetById(It.IsAny<int>()))
                         .Returns((int genreId) => _genres.FirstOrDefault(g => g.Id == genreId));
        }

        public static void MockCreate()
        {
            GenreRepoMock.Setup(x => x.Create(It.IsAny<Genre>()))
                         .Returns(_genres.Last().Id + 1);
        }
        public static void MockUpdate()
        {
            GenreRepoMock.Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Genre>()));

        }
        public static void MockDelete()
        {
            GenreRepoMock.Setup(x => x.Delete(It.IsAny<int>()));
        }
    }
}

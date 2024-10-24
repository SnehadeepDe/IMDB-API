using IMDB_API.Test;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;
using IMDB_API.test.MockResources;
using IMDB_API.test.StepFiles;

namespace IMDB_API.Test.StepFiles
{
    [Scope (Feature = "Movie Resource")]
    [Binding]
    public class MovieSteps : BaseSteps
    {
        public MovieSteps(CustomWebApplicationFactory factory)
            : base(factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => MovieMock.MovieRepoMock.Object)
                    .AddScoped(_ => ActorMock.ActorRepoMock.Object)
                    .AddScoped(_ => ProducerMock.ProducerRepoMock.Object)
                    .AddScoped(_ => GenreMock.GenreRepoMock.Object)
                    .AddScoped(_ => ReviewMock.ReviewRepoMock.Object);
                });
            }))
        {
        }

        [BeforeScenario]
        public static void Mocks()
        {
            MovieMock.MockGetAll();
            MovieMock.MockGetById();
            MovieMock.MockCreate();
            MovieMock.MockUpdate();
            MovieMock.MockDelete();
            ActorMock.MockGetAll();
            GenreMock.MockGetAll();
        }
    }
}

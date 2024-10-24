using IMDB_API.Test;
using System;
using TechTalk.SpecFlow;
using Microsoft.Extensions.DependencyInjection;
using IMDB_API.test.MockResources;

namespace IMDB_API.test.StepFiles
{
    [Scope(Feature = "Genre Resource")]
    [Binding]
    public class GenreSteps : BaseSteps
    {
        public GenreSteps(CustomWebApplicationFactory factory)
            : base(factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => GenreMock.GenreRepoMock.Object);
                });
            }))
        {
        }
        [BeforeScenario]
        public static void Mocks()
        {
            GenreMock.MockGetAll();
            GenreMock.MockGetById();
            GenreMock.MockCreate();
            GenreMock.MockUpdate();
            GenreMock.MockDelete();
        }
    }
}

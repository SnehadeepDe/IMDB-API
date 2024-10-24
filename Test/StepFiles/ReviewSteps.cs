using IMDB_API.test.MockResources;
using IMDB_API.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Microsoft.Extensions.DependencyInjection;

namespace IMDB_API.test.StepFiles
{
    [Scope(Feature = "Review Resource")]
    [Binding]
    public class ReviewSteps : BaseSteps
    {
        public ReviewSteps(CustomWebApplicationFactory factory)
            : base(factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => ReviewMock.ReviewRepoMock.Object)
                    .AddScoped(_ => MovieMock.MovieRepoMock.Object);
                });
            }))
        {
        }

        [BeforeScenario]
        public static void Mocks()
        {
            ReviewMock.MockGetAll();
            ReviewMock.MockGetById();
            ReviewMock.MockCreate();
            ReviewMock.MockUpdate();
            ReviewMock.MockDelete();
            MovieMock.MockGetById();
        }
    }
}

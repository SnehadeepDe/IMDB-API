using IMDB_API.test.MockResources;
using IMDB_API.Test;
using Microsoft.Extensions.DependencyInjection;
using TechTalk.SpecFlow;

namespace IMDB_API.test.StepFiles
{
    [Scope(Feature = "Producer Resource")]
    [Binding]
    public class ProducerSteps : BaseSteps
    {
        public ProducerSteps(CustomWebApplicationFactory factory)
            : base(factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(_ => ProducerMock.ProducerRepoMock.Object);
                });
            }))
        {
        }

        [BeforeScenario]
        public static void Mocks()
        {
            ProducerMock.MockGetAll();
            ProducerMock.MockGetById();
            ProducerMock.MockCreate();
            ProducerMock.MockUpdate();
            ProducerMock.MockDelete();
        }
    }
}

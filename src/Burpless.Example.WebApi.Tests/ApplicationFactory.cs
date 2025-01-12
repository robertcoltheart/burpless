using Burpless.Example.WebApi.Tests.Rest;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Burpless.Example.WebApi.Tests;

internal class ApplicationFactory : WebApplicationFactory<Program>
{
    private static ApplicationFactory? instance;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .UseEnvironment("Integration")
            .ConfigureTestServices(services => services
                .AddSingleton<IWebApiClient>(_ => new WebApiClient(CreateDefaultClient())));
    }

    [Before(TestSession)]
    public static void BeforeTest()
    {
        instance = new ApplicationFactory();

        // Let Burpless resolve context dependencies using the services from the ASP.NET Core application
        BurplessSettings.Configure(configuration => configuration.UseServiceProvider(instance.Services));
    }
}

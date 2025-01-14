using Burpless.Example.WebApi.Tests.Rest;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Burpless.Example.WebApi.Tests;

public class ApplicationFactory : WebApplicationFactory<Program>
{
    private static ApplicationFactory? factory;

    [Before(TestSession)]
    public static void BeforeTest()
    {
        factory = new ApplicationFactory();

        // Let Burpless resolve context dependencies using the services from the ASP.NET Core application
        BurplessSettings.Configure(x => x.UseServiceProvider(factory.Services));
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services => services
            .AddSingleton(this)
            .AddSingleton<IWebApiClient>(x => new WebApiClient(CreateClient())));
    }
}

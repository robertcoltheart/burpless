using Microsoft.Extensions.DependencyInjection;

namespace Burpless.Tests;

public class WebApi
{
    [Before(TestSession)]
    public static Task InitializeAsync()
    {
        var services = new ServiceCollection()
            .BuildServiceProvider();

        Configuration.Initialize(x => x.UseServiceProvider(services));

        return Task.CompletedTask;
    }
}

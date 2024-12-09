using Microsoft.Extensions.DependencyInjection;

namespace Burpless.Tests;

public class WebApi : IAsyncLifetime
{
    public Task InitializeAsync()
    {
        var services = new ServiceCollection()
            .BuildServiceProvider();

        Configuration.Initialize(x => x.UseServiceProvider(services));

        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}

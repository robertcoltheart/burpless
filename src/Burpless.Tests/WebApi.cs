using Microsoft.Extensions.DependencyInjection;

namespace Burpless.Tests;

public class WebApi : IAsyncLifetime
{
    public ValueTask InitializeAsync()
    {
        var services = new ServiceCollection()
            .BuildServiceProvider();

        Configuration.Initialize(x => x.UseServiceProvider(services));

        return ValueTask.CompletedTask;
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }
}

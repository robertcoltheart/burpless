using Microsoft.Extensions.DependencyInjection;
using TUnit.Core.Interfaces;

namespace Burpless.Tests;

public class WebApi : IAsyncInitializer, IAsyncDisposable
{
    public Task InitializeAsync()
    {
        var services = new ServiceCollection()
            .BuildServiceProvider();

        Configuration.Initialize(x => x.UseServiceProvider(services));

        return Task.CompletedTask;
    }

    public ValueTask DisposeAsync()
    {
        return ValueTask.CompletedTask;
    }
}

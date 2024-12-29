using System.Text;
using Burpless.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Burpless.Tests.Services;

public class SimpleServiceProviderTests
{
    private readonly SimpleServiceProvider provider = new();

    [Test]
    public async Task CanResolveClass()
    {
        var result = provider.GetService<Standalone>();

        await Assert.That(result).IsNotNull();
    }

    [Test]
    public async Task CanResolveClassAndDependencies()
    {
        var result = provider.GetService<WithDependency>();

        await Assert.That(result?.Standalone).IsNotNull();
    }

    [Test]
    public async Task SameDependencyIsInjectedTwice()
    {
        var result = provider.GetService<WithChainedDependencies>();

        await Assert.That(result?.Dependency).IsNotNull();
        await Assert.That(result?.Standalone).IsNotNull();
        await Assert.That(result?.Dependency.Standalone).IsEqualTo(result?.Standalone);
    }

    [Test]
    public void CannotResolveInterface()
    {
        Assert.Throws(() => provider.GetService<IServiceProvider>());
    }

    [Test]
    public void CannotResolveAbstract()
    {
        Assert.Throws(() => provider.GetService<Encoding>());
    }

    private record Standalone;

    private record WithDependency(Standalone Standalone);

    private record WithChainedDependencies(Standalone Standalone, WithDependency Dependency);
}

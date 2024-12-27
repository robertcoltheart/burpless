using System.Text;
using Burpless.Services;
using Microsoft.Extensions.DependencyInjection;
using Assert = Xunit.Assert;

namespace Burpless.Tests.Services;

public class SimpleServiceProviderTests
{
    private readonly SimpleServiceProvider provider = new();

    [Fact]
    public void CanResolveClass()
    {
        var result = provider.GetService<Standalone>();

        Assert.NotNull(result);
    }

    [Fact]
    public void CanResolveClassAndDependencies()
    {
        var result = provider.GetService<WithDependency>();

        Assert.NotNull(result);
        Assert.NotNull(result.Standalone);
    }

    [Fact]
    public void SameDependencyIsInjectedTwice()
    {
        var result = provider.GetService<WithChainedDependencies>();

        Assert.NotNull(result);
        Assert.NotNull(result.Dependency);
        Assert.NotNull(result.Standalone);
        Assert.Same(result.Standalone, result.Dependency.Standalone);
    }

    [Fact]
    public void CannotResolveInterface()
    {
        var exception = Record.Exception(() => provider.GetService<IServiceProvider>());

        Assert.NotNull(exception);
    }

    [Fact]
    public void CannotResolveAbstract()
    {
        var exception = Record.Exception(() => provider.GetService<Encoding>());

        Assert.NotNull(exception);
    }

    private record Standalone;

    private record WithDependency(Standalone Standalone);

    private record WithChainedDependencies(Standalone Standalone, WithDependency Dependency);
}

using Burpless.Services;

namespace Burpless.Tests.Services;

public class HybridServiceProviderTests
{
    [Fact]
    public void CanResolveClassWithRegisteredDependencies()
    {
        var services = new SpecificProvider(typeof(TimeProvider), TimeProvider.System);
        var provider = new HybridServiceProvider(services);

        var service = provider.GetService(typeof(ClassWithDependency)) as ClassWithDependency;

        Assert.NotNull(service);
        Assert.NotNull(service.Time);
    }

    [Fact]
    public void ResolutionOfDependenciesNotRegisteredFails()
    {
        var services = new SpecificProvider(typeof(TimeProvider), TimeProvider.System);
        var provider = new HybridServiceProvider(services);

        var exception = Record.Exception(() => provider.GetService(typeof(ClassWithUnknownDependency)));

        Assert.NotNull(exception);
    }

    private record ClassWithDependency(TimeProvider Time);

    private record ClassWithUnknownDependency(IFormattable Property);

    private class SpecificProvider(Type type, object instance) : IServiceProvider
    {
        public object? GetService(Type serviceType)
        {
            return serviceType == type
                ? instance
                : null;
        }
    }
}

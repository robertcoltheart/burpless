using Burpless.Services;

namespace Burpless.Tests.Services;

public class HybridServiceProviderTests
{
    [Test]
    public async Task CanResolveClassWithRegisteredDependencies()
    {
        var services = new SpecificProvider(typeof(TimeProvider), TimeProvider.System);
        var provider = new HybridServiceProvider(services);

        var service = provider.GetService(typeof(ClassWithDependency)) as ClassWithDependency;

        await Assert.That(service?.Time).IsNotNull();
    }

    [Test]
    public void ResolutionOfDependenciesNotRegisteredFails()
    {
        var services = new SpecificProvider(typeof(TimeProvider), TimeProvider.System);
        var provider = new HybridServiceProvider(services);

        Assert.Throws(() => provider.GetService(typeof(ClassWithUnknownDependency)));
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

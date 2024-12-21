namespace Burpless.Services;

internal class HybridServiceProvider(IServiceProvider services) : IServiceProvider
{
    public object GetService(Type serviceType)
    {
        return services.GetService(serviceType) ?? Resolve(serviceType);
    }

    private object Resolve(Type type)
    {
        var constructor = type
            .GetConstructors()
            .OrderByDescending(x => x.GetParameters().Length)
            .FirstOrDefault();

        if (constructor == null)
        {
            throw new InvalidOperationException($"Cannot resolve {type}, no public constructors found");
        }

        var parameters = constructor
            .GetParameters()
            .Select(x => GetRequiredService(x.ParameterType))
            .ToArray();

        return Activator.CreateInstance(type, parameters);
    }

    private object GetRequiredService(Type serviceType)
    {
        var service = services.GetService(serviceType);

        if (service == null)
        {
            throw new InvalidOperationException($"No service for type '{serviceType}' has been registered.");
        }

        return service;
    }
}

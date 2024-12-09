namespace Burpless.Services;

internal class WrappedServiceProvider(IServiceProvider services) : IServiceProvider
{
    public object GetService(Type serviceType)
    {
        var instance = services.GetService(serviceType);

        if (instance == null)
        {
            throw new InvalidOperationException();
        }

        return instance;
    }
}

using Burpless.Services;

namespace Burpless;

public class Configuration
{
    internal IServiceProvider Services { get; private set; } = new SimpleServiceProvider();

    internal static Configuration Instance { get; } = new();

    public static void Initialize(Action<Configuration> action)
    {
        action(Instance);
    }

    public Configuration UseServiceProvider(IServiceProvider provider)
    {
        Services = new WrappedServiceProvider(provider);

        return this;
    }
}

using System.Collections.Concurrent;
using Burpless.Publishing;
using Burpless.Services;
using Burpless.Tables;

namespace Burpless;

/// <summary>
/// Provides a mechanism for global configuration to be set.
/// </summary>
public class BurplessSettings
{
    internal static BurplessSettings Instance { get; } = new();

    internal IServiceProvider Services { get; private set; } = new SimpleServiceProvider();

    internal ConcurrentDictionary<Type, object> CustomParsers { get; } = new();

    internal List<IPublisher> Publishers { get; } = new();

    internal ScenarioCollector ScenariosCollector { get; } = new();

    /// <summary>
    /// Sets the global configuration values.
    /// </summary>
    /// <param name="action">A delegate for configuring the <see cref="BurplessSettings"/>.</param>
    public static void Configure(Action<BurplessSettings> action)
    {
        action(Instance);
    }

    /// <summary>
    /// Sets service provider to use for resolving scenario contexts and any injected services they use.
    /// </summary>
    /// <param name="provider">The service provider to use when resolving contexts and their injected services.</param>
    /// <returns>The configuration.</returns>
    public BurplessSettings UseServiceProvider(IServiceProvider provider)
    {
        Services = new HybridServiceProvider(provider);

        return this;
    }

    /// <summary>
    /// Adds a custom type parser when parsing <see cref="Table"/> values from string to a typed value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="parser">The parser instance to use when parsing from string to a typed value.</param>
    /// <returns>The configuration.</returns>
    public BurplessSettings AddCustomParser<T>(IParser<T> parser)
    {
        CustomParsers.AddOrUpdate(typeof(T), parser, (_, _) => parser);

        return this;
    }

    /// <summary>
    /// Adds a publisher that is used when publishing features and scenarios for the purpose of documentation.
    /// </summary>
    /// <param name="publisher">The publisher to use when publishing documentation.</param>
    /// <returns>The configuration.</returns>
    public BurplessSettings AddPublisher(IPublisher publisher)
    {
        Publishers.Add(publisher);

        return this;
    }
}

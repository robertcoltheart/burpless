﻿using Burpless.Services;

namespace Burpless;

/// <summary>
/// Provides a mechanism for global configuration to be set.
/// </summary>
public class Configuration
{
    internal IServiceProvider Services { get; private set; } = new SimpleServiceProvider();

    internal static Configuration Instance { get; } = new();

    /// <summary>
    /// Sets the global configuration values.
    /// </summary>
    /// <param name="action">A delegate for configuring the <see cref="Configuration"/>.</param>
    public static void Initialize(Action<Configuration> action)
    {
        action(Instance);
    }

    /// <summary>
    /// Sets service provider to use for resolving scenario contexts and any injected services they use.
    /// </summary>
    /// <param name="provider">The service provider to use when resolving contexts and their injected services.</param>
    /// <returns>The configuration.</returns>
    public Configuration UseServiceProvider(IServiceProvider provider)
    {
        Services = new HybridServiceProvider(provider);

        return this;
    }
}

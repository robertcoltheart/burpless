using System.Runtime.CompilerServices;
using Burpless.Builders;

namespace Burpless;

/// <summary>
/// Provides a set of methods for creating scenarios.
/// </summary>
public static class Scenario
{
    /// <summary>
    /// Creates a test scenario using the specified context type and scenario name.
    /// </summary>
    /// <typeparam name="TContext">The type of the context to use when running scenario steps.</typeparam>
    /// <param name="name">The specified name of the scenario.</param>
    /// <returns>The scenario builder.</returns>
    public static ScenarioGroupingBuilder<TContext> For<TContext>([CallerMemberName] string? name = null)
        where TContext : class
    {
        return new ScenarioGroupingBuilder<TContext>(name);
    }
}

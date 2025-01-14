using System.Runtime.CompilerServices;
using Burpless.Builders;

namespace Burpless;

/// <summary>
/// Provides a set of methods for creating scenarios.
/// </summary>
public class Scenario
{
    internal Scenario(string name, string? description, string[]? tags, ScenarioStep[] steps)
    {
        Name = name;
        Description = description;
        Tags = tags ?? [];
        Steps = steps;
    }

    /// <summary>
    /// Gets the name of the feature.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the detailed description of the feature.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Gets the set of tags belonging to the feature.
    /// </summary>
    public IReadOnlyCollection<string> Tags { get; }

    /// <summary>
    /// Gets the list of steps that belong to this scenario.
    /// </summary>
    public IReadOnlyCollection<ScenarioStep> Steps { get; }

    /// <summary>
    /// Creates a test scenario using the specified context type and scenario name.
    /// </summary>
    /// <typeparam name="TContext">The type of the context to use when running scenario steps.</typeparam>
    /// <param name="name">The specified name of the scenario.</param>
    /// <param name="filePath">The specified filename that contains this scenario.</param>
    /// <returns>The scenario builder.</returns>
    public static ScenarioGroupingBuilder<TContext> For<TContext>([CallerMemberName] string? name = null, [CallerFilePath] string? filePath = null)
        where TContext : class
    {
        return new ScenarioGroupingBuilder<TContext>(name, filePath);
    }
}

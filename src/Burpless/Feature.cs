using Burpless.Builders;

namespace Burpless;

/// <summary>
/// Specifies a group of scenarios that can share common background steps.
/// </summary>
public class Feature : IEquatable<Feature>
{
    private Feature()
    {
    }

    /// <summary>
    /// Gets the name of the feature.
    /// </summary>
    public string Name { get; private set; } = null!;

    /// <summary>
    /// Gets the detailed description of the feature.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Gets the set of tags belonging to the feature.
    /// </summary>
    public IReadOnlyCollection<string> Tags { get; private set; } = [];

    internal IReadOnlyCollection<IScenarioStep>? Steps { get; private set; }

    /// <summary>
    /// Creates a feature.
    /// </summary>
    /// <returns>The feature.</returns>
    public static Feature For()
    {
        return new Feature();
    }

    /// <summary>
    /// Creates a feature with the specified name.
    /// </summary>
    /// <param name="name">The name of the feature.</param>
    /// <returns>The feature.</returns>
    public Feature Named(string name)
    {
        Name = name;

        return this;
    }

    /// <summary>
    /// Sets the description of the feature.
    /// </summary>
    /// <param name="description">The specified description of the feature.</param>
    /// <returns>The feature.</returns>
    public Feature DescribedBy(string description)
    {
        Description = description;

        return this;
    }

    /// <summary>
    /// Sets the tags belonging to the feature.
    /// </summary>
    /// <param name="tags">The collection of tags belonging to the feature.</param>
    /// <returns>The feature.</returns>
    public Feature WithTags(params IEnumerable<string> tags)
    {
        Tags = [..tags];

        return this;
    }

    /// <summary>
    /// Adds background steps to the feature.
    /// </summary>
    /// <typeparam name="TContext">The type of the context to use for providing steps to the feature background.</typeparam>
    /// <param name="action">A delegate for configuring the <see cref="Feature"/>.</param>
    /// <returns>The feature.</returns>
    public Feature Background<TContext>(Action<BackgroundBuilder<TContext>> action)
        where TContext : class
    {
        var builder = new BackgroundBuilder<TContext>();
        action(builder);

        Steps ??= [];

        Steps = Steps.Concat(builder.Steps).ToArray();

        return this;
    }

    /// <inheritdoc/>
    public bool Equals(Feature? other)
    {
        return other?.Name == Name;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is Feature feature && Equals(feature);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}

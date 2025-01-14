namespace Burpless.Builders;

/// <summary>
/// Provides methods to configure how scenarios are grouped together.
/// </summary>
/// <typeparam name="TContext">The type of the context to use when running scenario steps.</typeparam>
public class ScenarioGroupingBuilder<TContext> : DescriptionBuilder<TContext>
    where TContext : class
{
    internal ScenarioGroupingBuilder(string? name, string? impliedFeature = null)
    {
        Details.Name = name;
        Details.ImpliedFeature = impliedFeature;
    }

    /// <summary>
    /// Sets the feature that this scenario belongs to.
    /// </summary>
    /// <param name="feature">The feature that the scenario belongs to.</param>
    /// <returns>The scenario builder.</returns>
    public DescriptionBuilder<TContext> ForFeature(Feature feature)
    {
        Details.Feature = feature;

        return this;
    }
}

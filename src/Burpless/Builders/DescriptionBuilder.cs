namespace Burpless.Builders;

/// <summary>
/// Provides a set of methods to configure the name, description and tags of the scenario.
/// </summary>
/// <typeparam name="TContext">The type of the context that is used when running scenario steps.</typeparam>
public class DescriptionBuilder<TContext> : GivenWhenThenBuilder<TContext>
    where TContext : class
{
    internal DescriptionBuilder()
    {
    }

    /// <summary>
    /// Sets the name of the scenario to the specified value.
    /// </summary>
    /// <param name="name">The specified name of the scenario.</param>
    /// <returns>The scenario builder.</returns>
    public DescriptionBuilder<TContext> Name(string name)
    {
        Details.Name = name;

        return this;
    }

    /// <summary>
    /// Sets the description of the scenario to the specified value.
    /// </summary>
    /// <param name="description">The specified description of the scenario.</param>
    /// <returns>The scenario builder.</returns>
    public DescriptionBuilder<TContext> Description(string description)
    {
        Details.Description = description;

        return this;
    }

    /// <summary>
    /// Sets the tags of the scenario to the specified values.
    /// </summary>
    /// <param name="tags">The specified tags of the scenario.</param>
    /// <returns>The scenario builder.</returns>
    public DescriptionBuilder<TContext> Tags(params IEnumerable<string> tags)
    {
        Details.Tags = tags.ToArray();

        return this;
    }
}

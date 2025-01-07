using Burpless.Builders;

namespace Burpless;

public class Feature : IEquatable<Feature>
{
    private Feature()
    {
    }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public IReadOnlyCollection<string> Tags { get; private set; }

    internal IReadOnlyCollection<IScenarioStep>? Steps { get; private set; }

    public static Feature Named(string name)
    {
        return new Feature
        {
            Name = name
        };
    }

    public Feature WithDescription(string description)
    {
        Description = description;

        return this;
    }

    public Feature WithTags(params IEnumerable<string> tags)
    {
        Tags = [..tags];

        return this;
    }

    public Feature WithBackground<TContext>(Action<BackgroundBuilder<TContext>> action)
        where TContext : class
    {
        var builder = new BackgroundBuilder<TContext>();
        action(builder);

        Steps = builder.Steps;

        return this;
    }

    public bool Equals(Feature? other)
    {
        return other?.Name == Name;
    }

    public override bool Equals(object? obj)
    {
        return obj is Feature feature && Equals(feature);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}

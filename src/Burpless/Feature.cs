namespace Burpless;

public class Feature : IEquatable<Feature>
{
    public string Name { get; private set; }

    public string? Description { get; private set; }

    public IReadOnlyCollection<string> Tags { get; private set; }

    public static Feature Named(string name)
    {
        return new Feature
        {
            Name = name
        };
    }

    public static IFeatureBackgroundBuilder<TContext> Background<TContext>()
        where TContext : class
    {
        return null;
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

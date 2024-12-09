namespace Burpless;

public class Feature : IEquatable<Feature>
{
    public string Name { get; private set; }

    public string Description { get; private set; }

    public IReadOnlyCollection<string> Tags { get; private set; }

    public static Feature Empty()
    {
        return new Feature
        {
            Name = string.Empty,
            Description = string.Empty,
            Tags = []
        };
    }

    public static Feature New(string name, string? description = null, params IEnumerable<string> tags)
    {
        return new Feature
        {
            Name = name,
            Description = description,
            Tags = [..tags]
        };
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

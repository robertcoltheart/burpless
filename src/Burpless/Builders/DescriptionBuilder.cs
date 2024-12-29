namespace Burpless.Builders;

public class DescriptionBuilder<TContext> : GivenWhenThenBuilder<TContext>
    where TContext : class
{
    internal DescriptionBuilder()
    {
    }

    public DescriptionBuilder<TContext> Name(string name)
    {
        Details.Name = name;

        return this;
    }

    public DescriptionBuilder<TContext> Description(string description)
    {
        Details.Description = description;

        return this;
    }

    public DescriptionBuilder<TContext> Tags(params IEnumerable<string> tags)
    {
        Details.Tags = tags.ToArray();

        return this;
    }

    public DescriptionBuilder<TContext> Tags(params string[] tags)
    {
        Details.Tags = tags;

        return this;
    }
}

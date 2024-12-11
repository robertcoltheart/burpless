namespace Burpless.Builders;

public interface IDescriptionBuilder<TContext> : IGivenBuilder<TContext>
    where TContext : class
{
    IDescriptionBuilder<TContext> Name(string name);

    IDescriptionBuilder<TContext> Description(string name);

    IDescriptionBuilder<TContext> Tags(params IEnumerable<string> tags);

    IDescriptionBuilder<TContext> Tags(params string[] tags);
}

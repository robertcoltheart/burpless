namespace Burpless;

public interface IDescriptionBuilder<TContext> : IScenarioBuilder<TContext>
    where TContext : class
{
    IDescriptionBuilder<TContext> Name(string name);

    IDescriptionBuilder<TContext> Description(string name);

    IDescriptionBuilder<TContext> Tags(params IEnumerable<string> tags);
}

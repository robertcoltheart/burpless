namespace Burpless;

public interface INameBuilder<TContext> : IScenarioBuilder<TContext>
    where TContext : class
{
    INameBuilder<TContext> Name(string name);

    INameBuilder<TContext> Description(string name);

    INameBuilder<TContext> Tags(params IEnumerable<string> tags);
}

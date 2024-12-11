namespace Burpless.Builders;

public interface IScenarioGroupingBuilder<TContext> : IDescriptionBuilder<TContext>
    where TContext : class
{
    IDescriptionBuilder<TContext> Feature(Feature feature);
}

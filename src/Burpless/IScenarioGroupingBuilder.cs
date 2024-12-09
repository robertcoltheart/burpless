namespace Burpless;

public interface IScenarioGroupingBuilder<TContext> : IDescriptionBuilder<TContext>
    where TContext : class
{
    IFeatureBuilder<TContext> Feature(Feature feature);
}

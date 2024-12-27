namespace Burpless.Builders;

public class ScenarioGroupingBuilder<TContext> : DescriptionBuilder<TContext>
    where TContext : class
{
    public DescriptionBuilder<TContext> Feature(Feature feature)
    {
        Details.Feature = feature;

        return this;
    }
}

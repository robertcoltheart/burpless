namespace Burpless.Builders;

public class ScenarioGroupingBuilder<TContext> : DescriptionBuilder<TContext>
    where TContext : class
{
    internal ScenarioGroupingBuilder(string? name)
    {
        Details.Name = name;
    }

    public DescriptionBuilder<TContext> Feature(Feature feature)
    {
        Details.Feature = feature;

        return this;
    }
}

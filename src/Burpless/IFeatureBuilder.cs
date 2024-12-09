namespace Burpless;

public interface IFeatureBuilder<TContext> : IDescriptionBuilder<TContext>
    where TContext : class
{
    IDescriptionBuilder<TContext> Background(IFeatureBackgroundBuilder<TContext> background);
}

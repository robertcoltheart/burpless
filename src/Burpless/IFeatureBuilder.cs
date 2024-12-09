namespace Burpless;

public interface IFeatureBuilder<TContext> : INameBuilder<TContext>
    where TContext : class
{
    INameBuilder<TContext> Feature(Feature feature);
}

using System.Collections.Concurrent;

namespace Burpless.Runner;

internal class ScenarioRunner<TContext>(IServiceProvider services, ScenarioDetails<TContext> details)
    where TContext : class
{
    private readonly ConcurrentDictionary<Type, object> contexts = new();

    public async Task Execute()
    {
        if (details.Feature?.Steps != null)
        {
            foreach (var step in details.Feature.Steps)
            {
                var featureContext = GetContext(step.ContextType);

                await step.Execute(featureContext);
            }
        }

        var context = GetContext(typeof(TContext));

        foreach (var step in details.Steps)
        {
            await step.Execute(context);
        }
    }

    private object GetContext(Type contextType)
    {
        return contexts.GetOrAdd(contextType, _ => services.GetService(contextType)!);
    }
}

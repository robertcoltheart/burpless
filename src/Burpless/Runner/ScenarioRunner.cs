using System.Collections.Concurrent;

namespace Burpless.Runner;

internal class ScenarioRunner(IServiceProvider services, ScenarioDetails details)
{
    private readonly ConcurrentDictionary<Type, object> contexts = new();

    public async Task Execute()
    {
        var featureSteps = details.Feature?.Steps ?? Array.Empty<IScenarioStep>();
        var steps = featureSteps.Concat(details.Steps);

        foreach (var step in steps)
        {
            var context = GetContext(step.ContextType);

            await step.Execute(context);
        }
    }

    private object GetContext(Type contextType)
    {
        return contexts.GetOrAdd(contextType, _ => services.GetService(contextType)!);
    }
}

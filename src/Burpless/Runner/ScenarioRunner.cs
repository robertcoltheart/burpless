using System.Collections.Concurrent;

namespace Burpless.Runner;

internal class ScenarioRunner(IServiceProvider services, ScenarioDetails details)
{
    private readonly BurplessSettings settings = BurplessSettings.Instance;

    private readonly ConcurrentDictionary<Type, object> contexts = new();

    public async Task Execute()
    {
        var feature = details.Feature ?? Feature.Named(details.ImpliedFeature ?? "Unknown");
        var steps = feature.BackgroundSteps.Concat(details.Steps);

        foreach (var step in steps)
        {
            var context = GetContext(step.ContextType);

            await step.Execute(context).ConfigureAwait(false);
        }

        if (settings.Publishers.Any())
        {
            var scenario = new Scenario(details.Name ?? "Unknown", details.Description, details.Tags, details.Steps.ToArray());

            settings.ScenariosCollector.Collect(feature, scenario);
        }
    }

    private object GetContext(Type contextType)
    {
        return contexts.GetOrAdd(contextType, _ => services.GetService(contextType)!);
    }
}

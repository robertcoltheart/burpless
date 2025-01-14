using System.Collections.Concurrent;

namespace Burpless.Publishing;

internal class ScenarioCollector
{
    private readonly ConcurrentDictionary<Feature, List<Scenario>> features = new();

    public void Collect(Feature feature, Scenario scenario)
    {
        var scenarios = features.GetOrAdd(feature, _ => []);

        scenarios.Add(scenario);
    }

    public async Task PublishAll(IPublisher publisher, Predicate<Feature>? predicate)
    {
        foreach (var feature in features.Keys)
        {
            if (predicate?.Invoke(feature) == false)
            {
                continue;
            }

            var scenarios = features[feature];

            await publisher.Publish(feature, scenarios).ConfigureAwait(false);
        }
    }
}

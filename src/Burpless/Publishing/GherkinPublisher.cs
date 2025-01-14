namespace Burpless.Publishing;

/// <summary>
/// A simple publisher that outputs Gherkin syntax for features and scenarios.
/// </summary>
public class GherkinPublisher : IPublisher
{
    /// <inheritdoc />
    public Task Publish(Feature feature, IReadOnlyList<Scenario> scenarios)
    {
        throw new NotImplementedException();
    }
}

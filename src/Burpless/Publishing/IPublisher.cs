namespace Burpless.Publishing;

/// <summary>
/// Defines a publisher that can be used for publishing features and scenarios for the purpose of documentation.
/// </summary>
public interface IPublisher
{
    /// <summary>
    /// Publish a feature with all associated scenarios.
    /// </summary>
    /// <param name="feature">The feature to publish.</param>
    /// <param name="scenarios">A collection of scenarios belonging to the feature.</param>
    /// <returns>A task that represents the asynchronous publish operation.</returns>
    Task Publish(Feature feature, IReadOnlyList<Scenario> scenarios);
}

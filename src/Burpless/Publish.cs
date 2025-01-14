namespace Burpless;

/// <summary>
/// Provides a set of methods for publishing feature documentation.
/// </summary>
public static class Publish
{
    /// <summary>
    /// Publish features collected during the test run to the configured publishers with the specified filter.
    /// </summary>
    /// <param name="predicate">An optional filter on features.</param>
    public static async Task Features(Predicate<Feature>? predicate = null)
    {
        var settings = BurplessSettings.Instance;

        foreach (var publisher in settings.Publishers)
        {
            await settings.ScenariosCollector.PublishAll(publisher, predicate).ConfigureAwait(false);
        }
    }
}

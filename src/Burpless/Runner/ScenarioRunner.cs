namespace Burpless.Runner;

internal class ScenarioRunner<T>(ScenarioDetails<T> details) : IScenarioRunner
{
    public Task Execute()
    {
        var provider = Configuration.Instance.Services;

        var context = provider.GetService(typeof(T));

        if (details.Feature != null)
        {

        }

        return Task.CompletedTask;
    }
}

namespace Burpless.Runner;

internal class ScenarioRunner<T>(ScenarioDetails<T> details) : IScenarioRunner
{
    public Task Execute()
    {
        return Task.CompletedTask;
    }
}

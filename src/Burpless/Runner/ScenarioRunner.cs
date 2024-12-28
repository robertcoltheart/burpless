namespace Burpless.Runner;

internal class ScenarioRunner<T>(ScenarioDetails<T> details, Feature? feature, ScenarioStep<T>[] steps, Type? expectedException) : IScenarioRunner
{
    public Task Execute()
    {
        return Task.CompletedTask;
    }
}

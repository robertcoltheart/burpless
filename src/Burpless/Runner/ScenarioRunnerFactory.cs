namespace Burpless.Runner;

internal class ScenarioRunnerFactory : IScenarioRunnerFactory
{
    public IScenarioRunner Create<T>(ScenarioDetails<T> details, Feature? feature, ScenarioStep<T>[] steps, Type? expectedException)
    {
        return new ScenarioRunner<T>(details, feature, steps, expectedException);
    }
}

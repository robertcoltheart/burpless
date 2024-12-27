namespace Burpless.Runner;

internal interface IScenarioRunnerFactory
{
    IScenarioRunner Create<T>(ScenarioDetails<T> details, Feature? feature, IScenarioStep[] steps, Type? expectedException);
}

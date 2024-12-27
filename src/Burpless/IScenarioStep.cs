namespace Burpless;

internal interface IScenarioStep
{
    string Name { get; }

    StepType Type { get; }
}

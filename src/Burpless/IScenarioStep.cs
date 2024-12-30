namespace Burpless;

internal interface IScenarioStep
{
    string Name { get; }

    StepType Type { get; }

    Type ContextType { get; }

    Task Execute(object context);
}

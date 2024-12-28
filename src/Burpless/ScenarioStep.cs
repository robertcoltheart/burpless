namespace Burpless;

internal class ScenarioStep<T>(string name, StepType type, Func<T, StepResult, Task> action)
{
    public string Name { get; } = name;

    public StepType Type { get; } = type;

    public Func<T, StepResult, Task> Action { get; } = action;
}

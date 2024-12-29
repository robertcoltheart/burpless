namespace Burpless;

internal class ScenarioStep<T>(string name, StepType type, Func<T, Task> action)
{
    public string Name { get; } = name;

    public StepType Type { get; } = type;

    public Func<T, Task> Action { get; } = action;
}

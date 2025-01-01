namespace Burpless;

internal class ScenarioStep<T>(string name, StepType type, Func<T, Task> action) : IScenarioStep
{
    public string Name { get; } = name;

    public StepType Type { get; } = type;

    public Type ContextType { get; } = typeof(T);

    public Task Execute(object context)
    {
        if (context is not T typedContext)
        {
            throw new ArgumentException($"Invalid context type, expected {typeof(T)} but got {context.GetType()}");
        }

        return action(typedContext);
    }
}

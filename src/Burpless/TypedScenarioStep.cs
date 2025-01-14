namespace Burpless;

internal class TypedScenarioStep<T>(string name, StepType type, Func<T, Task> action) : ScenarioStep(name, type)
{
    internal override Type ContextType { get; } = typeof(T);

    internal override Task Execute(object context)
    {
        if (context is not T typedContext)
        {
            throw new ArgumentException($"Invalid context type, expected {typeof(T)} but got {context.GetType()}");
        }

        return action(typedContext);
    }
}

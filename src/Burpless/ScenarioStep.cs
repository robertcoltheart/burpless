namespace Burpless;

/// <summary>
/// A step in a scenario.
/// </summary>
/// <param name="name"></param>
/// <param name="type"></param>
public abstract class ScenarioStep(string name, StepType type)
{
    /// <summary>
    /// Gets the name of the step.
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Gets the type of the step (given, when or then).
    /// </summary>
    public StepType Type { get; } = type;

    internal abstract Type ContextType { get; }

    internal abstract Task Execute(object context);
}

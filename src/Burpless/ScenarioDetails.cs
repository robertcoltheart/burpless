namespace Burpless;

internal class ScenarioDetails<TContext>
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string[]? Tags { get; set; }

    public Feature? Feature { get; set; }

    public Type? ExpectedException { get; set; }

    public List<ScenarioStep<TContext>> Steps { get; } = [];
}

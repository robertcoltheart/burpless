namespace Burpless;

internal class ScenarioDetails
{
    public string? Name { get; set; }

    public string? ImpliedFeature { get; set; }

    public string? Description { get; set; }

    public string[]? Tags { get; set; }

    public Feature? Feature { get; set; }

    public List<ScenarioStep> Steps { get; } = [];
}

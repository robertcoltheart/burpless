using System.Runtime.CompilerServices;

namespace Burpless.Builders;

public class ScenarioExecutor<TContext>
    where TContext : class
{
    internal ScenarioDetails<TContext> Details { get; set; } = new();

    public static implicit operator Task(ScenarioExecutor<TContext> executor)
    {
        return executor.ExecuteAsync();
    }

    public TaskAwaiter GetAwaiter()
    {
        return ExecuteAsync().GetAwaiter();
    }

    internal void AddStep(string name, StepType type, Func<TContext, StepResult, Task> action)
    {
        var scenarioStep = new ScenarioStep<TContext>(name, type, action);

        Details.Steps.Add(scenarioStep);
    }

    private Task ExecuteAsync()
    {
        return Task.CompletedTask;
    }
}

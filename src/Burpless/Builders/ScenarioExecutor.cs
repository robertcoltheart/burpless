﻿using System.Runtime.CompilerServices;
using Burpless.Runner;

namespace Burpless.Builders;

/// <summary>
/// Provides methods to execute the steps of a scenario.
/// </summary>
/// <typeparam name="TContext">The type of the context to use when running scenario steps.</typeparam>
public abstract class ScenarioExecutor<TContext>
    where TContext : class
{
    internal ScenarioExecutor()
    {
    }

    internal ScenarioDetails Details { get; set; } = new();

    /// <summary>
    /// Defines an implicit conversion of a <see cref="ScenarioExecutor{TContext}"/> to a <see cref="Task"/>.
    /// </summary>
    /// <param name="executor">A <see cref="ScenarioExecutor{TContext}"/> instance to be converted to a <see cref="Task"/>.</param>
    public static implicit operator Task(ScenarioExecutor<TContext> executor)
    {
        return executor.ExecuteAsync();
    }

    /// <summary>
    /// Gets an awaiter used to await this <see cref="ScenarioExecutor{TContext}" />.
    /// </summary>
    /// <returns>An awaiter instance.</returns>
    public TaskAwaiter GetAwaiter()
    {
        return ExecuteAsync().GetAwaiter();
    }

    internal void AddStep<T>(string name, StepType type, Func<T, Task> action)
        where T : class
    {
        var scenarioStep = new ScenarioStep<T>(name, type, action);

        Details.Steps.Add(scenarioStep);
    }

    private Task ExecuteAsync()
    {
        var runner = new ScenarioRunner(BurplessSettings.Instance.Services, Details);

        return runner.Execute();
    }
}

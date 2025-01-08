using System.Linq.Expressions;

namespace Burpless.Builders;

/// <summary>
/// Provides a set of methods to configure feature background steps.
/// </summary>
/// <typeparam name="TContext"></typeparam>
public class BackgroundBuilder<TContext>
    where TContext: class
{
    internal BackgroundBuilder()
    {
    }

    internal List<IScenarioStep> Steps { get; set; } = [];

    /// <summary>
    /// Adds a 'given' step to the background.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The background builder.</returns>
    public BackgroundContinuationBuilder<TContext> Given(Expression<Action<TContext>> action)
    {
        return Given(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a 'given' step to the background.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The background builder.</returns>
    public BackgroundContinuationBuilder<TContext> Given(Expression<Func<TContext, Task>> action)
    {
        return Given(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a 'given' step to the background with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The background builder.</returns>
    public BackgroundContinuationBuilder<TContext> Given(string step, Action<TContext> action)
    {
        return Given(step, action.ToAsync());
    }

    /// <summary>
    /// Adds a 'given' step to the background with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The background builder.</returns>
    public BackgroundContinuationBuilder<TContext> Given(string step, Func<TContext, Task> action)
    {
        var backgroundStep = new ScenarioStep<TContext>(step, StepType.Given, action);

        Steps.Add(backgroundStep);

        return new BackgroundContinuationBuilder<TContext>
        {
            Steps = Steps
        };
    }
}

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

    internal List<ScenarioStep> Steps { get; set; } = [];

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
        var backgroundStep = new TypedScenarioStep<TContext>(step, StepType.Given, action);

        Steps.Add(backgroundStep);

        return ContinueWith();
    }

    /// <summary>
    /// Adds a 'given' step to the background.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The background builder.</returns>
    public BackgroundContinuationBuilder<TContext> Given<TAdditionalContext>(Expression<Action<TAdditionalContext>> action)
        where TAdditionalContext : class
    {
        return Given(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a 'given' step to the background.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The background builder.</returns>
    public BackgroundContinuationBuilder<TContext> Given<TAdditionalContext>(Expression<Func<TAdditionalContext, Task>> action)
        where TAdditionalContext : class
    {
        return Given(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a 'given' step to the background with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The background builder.</returns>
    public BackgroundContinuationBuilder<TContext> Given<TAdditionalContext>(string step, Action<TAdditionalContext> action)
        where TAdditionalContext : class
    {
        return Given(step, action.ToAsync());
    }

    /// <summary>
    /// Adds a 'given' step to the background with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The background builder.</returns>
    public BackgroundContinuationBuilder<TContext> Given<TAdditionalContext>(string step, Func<TAdditionalContext, Task> action)
        where TAdditionalContext : class
    {
        var backgroundStep = new TypedScenarioStep<TAdditionalContext>(step, StepType.Given, action);

        Steps.Add(backgroundStep);

        return ContinueWith();
    }

    private BackgroundContinuationBuilder<TContext> ContinueWith()
    {
        return new BackgroundContinuationBuilder<TContext>
        {
            Steps = Steps
        };
    }
}

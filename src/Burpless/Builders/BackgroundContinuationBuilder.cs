using System.Linq.Expressions;

namespace Burpless.Builders;

/// <summary>
/// Provides a set of methods to configure feature background continuation steps.
/// </summary>
/// <typeparam name="TContext"></typeparam>
public class BackgroundContinuationBuilder<TContext> : BackgroundBuilder<TContext>
    where TContext : class
{
    internal BackgroundContinuationBuilder()
    {
    }

    /// <summary>
    /// Adds a 'given' step to the background.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The background builder.</returns>
    public BackgroundContinuationBuilder<TContext> And(Expression<Action<TContext>> action)
    {
        return Given(action);
    }

    /// <summary>
    /// Adds a 'given' step to the background.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The background builder.</returns>
    public BackgroundContinuationBuilder<TContext> And(Expression<Func<TContext, Task>> action)
    {
        return Given(action);
    }

    /// <summary>
    /// Adds a 'given' step to the background with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The background builder.</returns>
    public BackgroundContinuationBuilder<TContext> And(string step, Action<TContext> action)
    {
        return Given(step, action);
    }

    /// <summary>
    /// Adds a 'given' step to the background with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The background builder.</returns>
    public BackgroundContinuationBuilder<TContext> And(string step, Func<TContext, Task> action)
    {
        return Given(step, action);
    }

    /// <summary>
    /// Adds a 'given' step to the background.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The background builder.</returns>
    public BackgroundContinuationBuilder<TContext> But(Expression<Action<TContext>> action)
    {
        return Given(action);
    }

    /// <summary>
    /// Adds a 'given' step to the background.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The background builder.</returns>
    public BackgroundContinuationBuilder<TContext> But(Expression<Func<TContext, Task>> action)
    {
        return Given(action);
    }

    /// <summary>
    /// Adds a 'given' step to the background with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The background builder.</returns>
    public BackgroundContinuationBuilder<TContext> But(string step, Action<TContext> action)
    {
        return Given(step, action);
    }

    /// <summary>
    /// Adds a 'given' step to the background with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The background builder.</returns>
    public BackgroundContinuationBuilder<TContext> But(string step, Func<TContext, Task> action)
    {
        return Given(step, action);
    }
}

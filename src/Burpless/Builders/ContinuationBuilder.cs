using System.Linq.Expressions;

namespace Burpless.Builders;

/// <summary>
/// Provides methods to add 'given', 'when' and 'then' step definitions to a scenario as continuations.
/// </summary>
/// <typeparam name="TContext">The type of the context to use when running scenario steps.</typeparam>
public class ContinuationBuilder<TContext> : GivenWhenThenBuilder<TContext>
    where TContext : class
{
    internal ContinuationBuilder()
    {
    }

    /// <summary>
    /// Adds a step to the scenario that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> And(Expression<Action<TContext>> action)
    {
        return GetLastStepType() switch
        {
            StepType.Given => Given(action),
            StepType.When => When(action),
            StepType.Then => Then(action),
            _ => throw new ArgumentException("Step type not supported")
        };
    }

    /// <summary>
    /// Adds a step to the scenario that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> And(Expression<Func<TContext, Task>> action)
    {
        return GetLastStepType() switch
        {
            StepType.Given => Given(action),
            StepType.When => When(action),
            StepType.Then => Then(action),
            _ => throw new ArgumentException("Step type not supported")
        };
    }

    /// <summary>
    /// Adds a step to the scenario with the specified name that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> And(string step, Action<TContext> action)
    {
        return GetLastStepType() switch
        {
            StepType.Given => Given(step, action),
            StepType.When => When(step, action),
            StepType.Then => Then(step, action),
            _ => throw new ArgumentException("Step type not supported")
        };
    }

    /// <summary>
    /// Adds a step to the scenario with the specified name that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> And(string step, Func<TContext, Task> action)
    {
        return GetLastStepType() switch
        {
            StepType.Given => Given(step, action),
            StepType.When => When(step, action),
            StepType.Then => Then(step, action),
            _ => throw new ArgumentException("Step type not supported")
        };
    }

    /// <summary>
    /// Adds a step to the scenario that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> But(Expression<Action<TContext>> action)
    {
        return GetLastStepType() switch
        {
            StepType.Given => Given(action),
            StepType.When => When(action),
            StepType.Then => Then(action),
            _ => throw new ArgumentException("Step type not supported")
        };
    }

    /// <summary>
    /// Adds a step to the scenario that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> But(Expression<Func<TContext, Task>> action)
    {
        return GetLastStepType() switch
        {
            StepType.Given => Given(action),
            StepType.When => When(action),
            StepType.Then => Then(action),
            _ => throw new ArgumentException("Step type not supported")
        };
    }

    /// <summary>
    /// Adds a step to the scenario with the specified name that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> But(string step, Action<TContext> action)
    {
        return GetLastStepType() switch
        {
            StepType.Given => Given(step, action),
            StepType.When => When(step, action),
            StepType.Then => Then(step, action),
            _ => throw new ArgumentException("Step type not supported")
        };
    }

    /// <summary>
    /// Adds a step to the scenario with the specified name that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> But(string step, Func<TContext, Task> action)
    {
        return GetLastStepType() switch
        {
            StepType.Given => Given(step, action),
            StepType.When => When(step, action),
            StepType.Then => Then(step, action),
            _ => throw new ArgumentException("Step type not supported")
        };
    }

    private StepType GetLastStepType()
    {
        return Details.Steps.Last().Type;
    }
}

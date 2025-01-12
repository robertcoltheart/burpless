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
        return And(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a step to the scenario that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> And(Expression<Func<TContext, Task>> action)
    {
        return And(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a step to the scenario with the specified name that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> And(string step, Action<TContext> action)
    {
        return And(step, action.ToAsync());
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
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> And<TAdditionalContext>(Expression<Action<TAdditionalContext>> action)
        where TAdditionalContext : class
    {
        return And(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a step to the scenario that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> And<TAdditionalContext>(Expression<Func<TAdditionalContext, Task>> action)
        where TAdditionalContext : class
    {
        return And(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a step to the scenario with the specified name that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> And<TAdditionalContext>(string step, Action<TAdditionalContext> action)
        where TAdditionalContext : class
    {
        return And(step, action.ToAsync());
    }

    /// <summary>
    /// Adds a step to the scenario with the specified name that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> And<TAdditionalContext>(string step, Func<TAdditionalContext, Task> action)
        where TAdditionalContext : class
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
        return And(action);
    }

    /// <summary>
    /// Adds a step to the scenario that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> But(Expression<Func<TContext, Task>> action)
    {
        return And(action);
    }

    /// <summary>
    /// Adds a step to the scenario with the specified name that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> But(string step, Action<TContext> action)
    {
        return And(step, action);
    }

    /// <summary>
    /// Adds a step to the scenario with the specified name that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> But(string step, Func<TContext, Task> action)
    {
        return And(step, action);
    }

    /// <summary>
    /// Adds a step to the scenario that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> But<TAdditionalContext>(Expression<Action<TAdditionalContext>> action)
        where TAdditionalContext : class
    {
        return And(action);
    }

    /// <summary>
    /// Adds a step to the scenario that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> But<TAdditionalContext>(Expression<Func<TAdditionalContext, Task>> action)
        where TAdditionalContext : class
    {
        return And(action);
    }

    /// <summary>
    /// Adds a step to the scenario with the specified name that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> But<TAdditionalContext>(string step, Action<TAdditionalContext> action)
        where TAdditionalContext : class
    {
        return And(step, action);
    }

    /// <summary>
    /// Adds a step to the scenario with the specified name that is the same as the previous step in the sequence.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> But<TAdditionalContext>(string step, Func<TAdditionalContext, Task> action)
        where TAdditionalContext : class
    {
        return And(step, action);
    }

    private StepType GetLastStepType()
    {
        if (!Details.Steps.Any())
        {
            throw new ArgumentException("No previous step configured for continuing scenario.");
        }

        return Details.Steps.Last().Type;
    }
}

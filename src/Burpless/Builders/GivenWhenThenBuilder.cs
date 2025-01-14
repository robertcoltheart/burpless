﻿using System.Linq.Expressions;

namespace Burpless.Builders;

/// <summary>
/// Provides methods to add 'given', 'when' and 'then' step definitions to a scenario.
/// </summary>
/// <typeparam name="TContext">The type of the context to use when running scenario steps.</typeparam>
public class GivenWhenThenBuilder<TContext> : ScenarioExecutor<TContext>
    where TContext : class
{
    internal GivenWhenThenBuilder()
    {
    }

    /// <summary>
    /// Adds a 'given' step to the scenario.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> Given(Expression<Action<TContext>> action)
    {
        return Given(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a 'given' step to the scenario.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> Given(Expression<Func<TContext, Task>> action)
    {
        return Given(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a 'given' step to the scenario with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> Given(string step, Action<TContext> action)
    {
        return Given(step, action.ToAsync());
    }

    /// <summary>
    /// Adds a 'given' step to the scenario with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> Given(string step, Func<TContext, Task> action)
    {
        AddStep(step, StepType.Given, action);

        return ContinueWith();
    }

    /// <summary>
    /// Adds a 'given' step to the scenario.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> Given<TAdditionalContext>(Expression<Action<TAdditionalContext>> action)
        where TAdditionalContext : class
    {
        return Given(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a 'given' step to the scenario.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> Given<TAdditionalContext>(Expression<Func<TAdditionalContext, Task>> action)
        where TAdditionalContext : class
    {
        return Given(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a 'given' step to the scenario with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> Given<TAdditionalContext>(string step, Action<TAdditionalContext> action)
        where TAdditionalContext : class
    {
        return Given(step, action.ToAsync());
    }

    /// <summary>
    /// Adds a 'given' step to the scenario with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> Given<TAdditionalContext>(string step, Func<TAdditionalContext, Task> action)
        where TAdditionalContext : class
    {
        AddStep(step, StepType.Given, action);

        return ContinueWith();
    }

    /// <summary>
    /// Adds a 'when' step to the scenario.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> When(Expression<Action<TContext>> action)
    {
        return When(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a 'when' step to the scenario.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> When(Expression<Func<TContext, Task>> action)
    {
        return When(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a 'when' step to the scenario with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> When(string step, Action<TContext> action)
    {
        return When(step, action.ToAsync());
    }

    /// <summary>
    /// Adds a 'when' step to the scenario with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> When(string step, Func<TContext, Task> action)
    {
        AddStep(step, StepType.When, action);

        return ContinueWith();
    }

    /// <summary>
    /// Adds a 'when' step to the scenario.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> When<TAdditionalContext>(Expression<Action<TAdditionalContext>> action)
        where TAdditionalContext : class
    {
        return When(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a 'when' step to the scenario.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> When<TAdditionalContext>(Expression<Func<TAdditionalContext, Task>> action)
        where TAdditionalContext : class
    {
        return When(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a 'when' step to the scenario with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> When<TAdditionalContext>(string step, Action<TAdditionalContext> action)
        where TAdditionalContext : class
    {
        return When(step, action.ToAsync());
    }

    /// <summary>
    /// Adds a 'when' step to the scenario with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> When<TAdditionalContext>(string step, Func<TAdditionalContext, Task> action)
        where TAdditionalContext : class
    {
        AddStep(step, StepType.When, action);

        return ContinueWith();
    }

    /// <summary>
    /// Adds a 'then' step to the scenario.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> Then(Expression<Action<TContext>> action)
    {
        return Then(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a 'then' step to the scenario.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> Then(Expression<Func<TContext, Task>> action)
    {
        return Then(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a 'then' step to the scenario with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> Then(string step, Action<TContext> action)
    {
        return Then(step, action.ToAsync());
    }

    /// <summary>
    /// Adds a 'then' step to the scenario with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> Then(string step, Func<TContext, Task> action)
    {
        AddStep(step, StepType.Then, action);

        return ContinueWith();
    }

    /// <summary>
    /// Adds a 'then' step to the scenario.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> Then<TAdditionalContext>(Expression<Action<TAdditionalContext>> action)
        where TAdditionalContext : class
    {
        return Then(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a 'then' step to the scenario.
    /// </summary>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> Then<TAdditionalContext>(Expression<Func<TAdditionalContext, Task>> action)
        where TAdditionalContext : class
    {
        return Then(action.GetName(), action.Compile());
    }

    /// <summary>
    /// Adds a 'then' step to the scenario with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> Then<TAdditionalContext>(string step, Action<TAdditionalContext> action)
        where TAdditionalContext : class
    {
        return Then(step, action.ToAsync());
    }

    /// <summary>
    /// Adds a 'then' step to the scenario with the given name.
    /// </summary>
    /// <param name="step">The name of the step.</param>
    /// <param name="action">A delegate for configuring the step.</param>
    /// <typeparam name="TAdditionalContext">The type of the context to use when running scenario steps.</typeparam>
    /// <returns>The scenario builder.</returns>
    public ContinuationBuilder<TContext> Then<TAdditionalContext>(string step, Func<TAdditionalContext, Task> action)
        where TAdditionalContext : class
    {
        AddStep(step, StepType.Then, action);

        return ContinueWith();
    }

    private ContinuationBuilder<TContext> ContinueWith()
    {
        return new ContinuationBuilder<TContext>
        {
            Details = Details
        };
    }
}

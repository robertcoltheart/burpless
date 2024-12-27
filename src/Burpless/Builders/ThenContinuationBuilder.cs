using System.Linq.Expressions;

namespace Burpless.Builders;

public class ThenContinuationBuilder<TContext> : ThenBuilder<TContext>
    where TContext : class
{
    internal ThenContinuationBuilder(ScenarioDetails<TContext> details)
    {
        Details = details;
    }

    public ThenContinuationBuilder<TContext> And(Expression<Action<TContext>> action)
    {
        return Then(action);
    }

    public ThenContinuationBuilder<TContext> And(Expression<Action<TContext, StepResult>> action)
    {
        return Then(action);
    }

    public ThenContinuationBuilder<TContext> And(Expression<Func<TContext, Task>> action)
    {
        return Then(action);
    }

    public ThenContinuationBuilder<TContext> And(Expression<Func<TContext, StepResult, Task>> action)
    {
        return Then(action);
    }

    public ThenContinuationBuilder<TContext> And(string step, Action<TContext> action)
    {
        return Then(step, action);
    }

    public ThenContinuationBuilder<TContext> And(string step, Action<TContext, StepResult> action)
    {
        return Then(step, action);
    }

    public ThenContinuationBuilder<TContext> And(string step, Func<TContext, Task> action)
    {
        return Then(step, action);
    }

    public ThenContinuationBuilder<TContext> And(string step, Func<TContext, StepResult, Task> action)
    {
        return Then(step, action);
    }

    public ThenContinuationBuilder<TContext> But(Expression<Action<TContext>> action)
    {
        return Then(action);
    }

    public ThenContinuationBuilder<TContext> But(Expression<Action<TContext, StepResult>> action)
    {
        return Then(action);
    }

    public ThenContinuationBuilder<TContext> But(Expression<Func<TContext, Task>> action)
    {
        return Then(action);
    }

    public ThenContinuationBuilder<TContext> But(Expression<Func<TContext, StepResult, Task>> action)
    {
        return Then(action);
    }

    public ThenContinuationBuilder<TContext> But(string step, Action<TContext> action)
    {
        return Then(step, action);
    }

    public ThenContinuationBuilder<TContext> But(string step, Action<TContext, StepResult> action)
    {
        return Then(step, action);
    }

    public ThenContinuationBuilder<TContext> But(string step, Func<TContext, Task> action)
    {
        return Then(step, action);
    }

    public ThenContinuationBuilder<TContext> But(string step, Func<TContext, StepResult, Task> action)
    {
        return Then(step, action);
    }
}

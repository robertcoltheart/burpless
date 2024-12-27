using System.Linq.Expressions;

namespace Burpless.Builders;

public class WhenContinuationBuilder<TContext> : WhenBuilder<TContext>
    where TContext : class
{
    internal WhenContinuationBuilder(ScenarioDetails<TContext> details)
    {
        Details = details;
    }

    public WhenContinuationBuilder<TContext> And(Expression<Action<TContext>> action)
    {
        return When(action);
    }

    public WhenContinuationBuilder<TContext> And(Expression<Func<TContext, Task>> action)
    {
        return When(action);
    }

    public WhenContinuationBuilder<TContext> And(string step, Action<TContext> action)
    {
        return When(step, action);
    }

    public WhenContinuationBuilder<TContext> And(string step, Func<TContext, Task> action)
    {
        return When(step, action);
    }

    public WhenContinuationBuilder<TContext> But(Expression<Action<TContext>> action)
    {
        return When(action);
    }

    public WhenContinuationBuilder<TContext> But(Expression<Func<TContext, Task>> action)
    {
        return When(action);
    }

    public WhenContinuationBuilder<TContext> But(string step, Action<TContext> action)
    {
        return When(step, action);
    }

    public WhenContinuationBuilder<TContext> But(string step, Func<TContext, Task> action)
    {
        return When(step, action);
    }
}

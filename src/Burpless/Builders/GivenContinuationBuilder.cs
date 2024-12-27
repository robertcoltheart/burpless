using System.Linq.Expressions;

namespace Burpless.Builders;

public class GivenContinuationBuilder<TContext> : GivenBuilder<TContext>
    where TContext : class
{
    internal GivenContinuationBuilder(ScenarioDetails<TContext> details)
    {
        Details = details;
    }

    public GivenContinuationBuilder<TContext> And(Expression<Action<TContext>> action)
    {
        return Given(action);
    }

    public GivenContinuationBuilder<TContext> And(Expression<Func<TContext, Task>> action)
    {
        return Given(action);
    }

    public GivenContinuationBuilder<TContext> And(string step, Action<TContext> action)
    {
        return Given(step, action);
    }

    public GivenContinuationBuilder<TContext> And(string step, Func<TContext, Task> action)
    {
        return Given(step, action);
    }

    public GivenContinuationBuilder<TContext> But(Expression<Action<TContext>> action)
    {
        return Given(action);
    }

    public GivenContinuationBuilder<TContext> But(Expression<Func<TContext, Task>> action)
    {
        return Given(action);
    }

    public GivenContinuationBuilder<TContext> But(string step, Action<TContext> action)
    {
        return Given(step, action);
    }

    public GivenContinuationBuilder<TContext> But(string step, Func<TContext, Task> action)
    {
        return Given(step, action);
    }
}

using System.Linq.Expressions;

namespace Burpless.Builders;

public class BackgroundContinuationBuilder<TContext> : BackgroundBuilder<TContext>
    where TContext : class
{
    public BackgroundContinuationBuilder<TContext> And(Expression<Action<TContext>> action)
    {
        return Given(action);
    }

    public BackgroundContinuationBuilder<TContext> And(Expression<Func<TContext, Task>> action)
    {
        return Given(action);
    }

    public BackgroundContinuationBuilder<TContext> And(string step, Action<TContext> action)
    {
        return Given(step, action);
    }

    public BackgroundContinuationBuilder<TContext> And(string step, Func<TContext, Task> action)
    {
        return Given(step, action);
    }

    public BackgroundContinuationBuilder<TContext> But(Expression<Action<TContext>> action)
    {
        return Given(action);
    }

    public BackgroundContinuationBuilder<TContext> But(Expression<Func<TContext, Task>> action)
    {
        return Given(action);
    }

    public BackgroundContinuationBuilder<TContext> But(string step, Action<TContext> action)
    {
        return Given(step, action);
    }

    public BackgroundContinuationBuilder<TContext> But(string step, Func<TContext, Task> action)
    {
        return Given(step, action);
    }
}

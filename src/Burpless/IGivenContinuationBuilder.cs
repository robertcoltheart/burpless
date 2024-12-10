using System.Linq.Expressions;

namespace Burpless;

public interface IGivenContinuationBuilder<TContext> : IGivenBuilder<TContext>
    where TContext : class
{
    IGivenContinuationBuilder<TContext> And(Expression<Action<TContext>> action);

    IGivenContinuationBuilder<TContext> And(Expression<Func<TContext, Task>> action);

    IGivenContinuationBuilder<TContext> And(string step, Action<TContext> action);

    IGivenContinuationBuilder<TContext> And(string step, Func<TContext, Task> action);

    IGivenContinuationBuilder<TContext> But(Expression<Action<TContext>> action);

    IGivenContinuationBuilder<TContext> But(Expression<Func<TContext, Task>> action);

    IGivenContinuationBuilder<TContext> But(string step, Action<TContext> action);

    IGivenContinuationBuilder<TContext> But(string step, Func<TContext, Task> action);
}

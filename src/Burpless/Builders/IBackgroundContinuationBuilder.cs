using System.Linq.Expressions;

namespace Burpless.Builders;

public interface IBackgroundContinuationBuilder<TContext> : IBackgroundBuilder<TContext>
    where TContext : class
{
    IBackgroundContinuationBuilder<TContext> And(Expression<Action<TContext>> action);

    IBackgroundContinuationBuilder<TContext> And(Expression<Func<TContext, Task>> action);

    IBackgroundContinuationBuilder<TContext> And(string step, Action<TContext> action);

    IBackgroundContinuationBuilder<TContext> And(string step, Func<TContext, Task> action);

    IBackgroundContinuationBuilder<TContext> But(Expression<Action<TContext>> action);

    IBackgroundContinuationBuilder<TContext> But(Expression<Func<TContext, Task>> action);

    IBackgroundContinuationBuilder<TContext> But(string step, Action<TContext> action);

    IBackgroundContinuationBuilder<TContext> But(string step, Func<TContext, Task> action);
}

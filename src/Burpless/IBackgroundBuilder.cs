using System.Linq.Expressions;

namespace Burpless;

public interface IBackgroundBuilder<TContext>
    where TContext : class
{
    IBackgroundContinuationBuilder<TContext> Given(Expression<Action<TContext>> action);

    IBackgroundContinuationBuilder<TContext> Given(Expression<Func<TContext, Task>> action);

    IBackgroundContinuationBuilder<TContext> Given(string step, Action<TContext> action);

    IBackgroundContinuationBuilder<TContext> Given(string step, Func<TContext, Task> action);
}

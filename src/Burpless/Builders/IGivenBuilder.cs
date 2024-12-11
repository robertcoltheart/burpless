using System.Linq.Expressions;

namespace Burpless.Builders;

public interface IGivenBuilder<TContext> : IWhenBuilder<TContext>
    where TContext : class
{
    IGivenContinuationBuilder<TContext> Given(Expression<Action<TContext>> action);

    IGivenContinuationBuilder<TContext> Given(Expression<Func<TContext, Task>> action);

    IGivenContinuationBuilder<TContext> Given(string step, Action<TContext> action);

    IGivenContinuationBuilder<TContext> Given(string step, Func<TContext, Task> action);
}

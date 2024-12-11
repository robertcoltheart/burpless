using System.Linq.Expressions;

namespace Burpless.Builders;

public interface IWhenBuilder<TContext> : IThenBuilder<TContext>
    where TContext : class
{
    IWhenContinuationBuilder<TContext> When(Expression<Action<TContext>> action);

    IWhenContinuationBuilder<TContext> When(Expression<Func<TContext, Task>> action);

    IWhenContinuationBuilder<TContext> When(string step, Action<TContext> action);

    IWhenContinuationBuilder<TContext> When(string step, Func<TContext, Task> action);
}

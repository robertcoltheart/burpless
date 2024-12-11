using System.Linq.Expressions;

namespace Burpless.Builders;

public interface IWhenContinuationBuilder<TContext> : IWhenBuilder<TContext>
    where TContext : class
{
    IWhenContinuationBuilder<TContext> And(Expression<Action<TContext>> action);

    IWhenContinuationBuilder<TContext> And(Expression<Func<TContext, Task>> action);

    IWhenContinuationBuilder<TContext> And(string step, Action<TContext> action);

    IWhenContinuationBuilder<TContext> And(string step, Func<TContext, Task> action);

    IWhenContinuationBuilder<TContext> But(Expression<Action<TContext>> action);

    IWhenContinuationBuilder<TContext> But(Expression<Func<TContext, Task>> action);

    IWhenContinuationBuilder<TContext> But(string step, Action<TContext> action);

    IWhenContinuationBuilder<TContext> But(string step, Func<TContext, Task> action);
}

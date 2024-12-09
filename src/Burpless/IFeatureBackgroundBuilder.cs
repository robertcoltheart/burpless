using System.Linq.Expressions;

namespace Burpless;

public interface IFeatureBackgroundBuilder<TContext>
    where TContext : class
{
    IFeatureBackgroundBuilder<TContext> Given(Expression<Action<TContext>> action);

    IFeatureBackgroundBuilder<TContext> Given(Expression<Func<TContext, Task>> action);

    IFeatureBackgroundBuilder<TContext> Given(string step, Action<TContext> action);

    IFeatureBackgroundBuilder<TContext> Given(string step, Func<TContext, Task> action);

    IFeatureBackgroundBuilder<TContext> And(Expression<Action<TContext>> action);

    IFeatureBackgroundBuilder<TContext> And(Expression<Func<TContext, Task>> action);

    IFeatureBackgroundBuilder<TContext> And(string step, Action<TContext> action);

    IFeatureBackgroundBuilder<TContext> And(string step, Func<TContext, Task> action);

    IFeatureBackgroundBuilder<TContext> But(Expression<Action<TContext>> action);

    IFeatureBackgroundBuilder<TContext> But(Expression<Func<TContext, Task>> action);

    IFeatureBackgroundBuilder<TContext> But(string step, Action<TContext> action);

    IFeatureBackgroundBuilder<TContext> But(string step, Func<TContext, Task> action);
}

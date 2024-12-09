using System.Linq.Expressions;

namespace Burpless;

public interface IScenarioBuilder<TContext> : IScenarioExecutor<TContext>
    where TContext : class
{
    IScenarioBuilder<TContext> Given(Expression<Action<TContext>> action);

    IScenarioBuilder<TContext> Given(Expression<Func<TContext, Task>> action);

    IScenarioBuilder<TContext> Given(string step, Action<TContext> action);

    IScenarioBuilder<TContext> Given(string step, Func<TContext, Task> action);

    IScenarioBuilder<TContext> When(Expression<Action<TContext>> action);

    IScenarioBuilder<TContext> When(Expression<Func<TContext, Task>> action);

    IScenarioBuilder<TContext> When(string step, Action<TContext> action);

    IScenarioBuilder<TContext> When(string step, Func<TContext, Task> action);

    IScenarioBuilder<TContext> Then(Expression<Action<TContext>> action);

    IScenarioBuilder<TContext> Then(Expression<Action<TContext, StepResult>> action);

    IScenarioBuilder<TContext> Then(Expression<Func<TContext, Task>> action);

    IScenarioBuilder<TContext> Then(Expression<Func<TContext, StepResult, Task>> action);

    IScenarioBuilder<TContext> Then(string step, Action<TContext> action);

    IScenarioBuilder<TContext> Then(string step, Action<TContext, StepResult> action);

    IScenarioBuilder<TContext> Then(string step, Func<TContext, Task> action);

    IScenarioBuilder<TContext> Then(string step, Func<TContext, StepResult, Task> action);

    IScenarioBuilder<TContext> And(Expression<Action<TContext>> action);

    IScenarioBuilder<TContext> And(Expression<Func<TContext, Task>> action);

    IScenarioBuilder<TContext> And(string step, Action<TContext> action);

    IScenarioBuilder<TContext> And(string step, Func<TContext, Task> action);

    IScenarioBuilder<TContext> But(Expression<Action<TContext>> action);

    IScenarioBuilder<TContext> But(Expression<Func<TContext, Task>> action);

    IScenarioBuilder<TContext> But(string step, Action<TContext> action);

    IScenarioBuilder<TContext> But(string step, Func<TContext, Task> action);
}

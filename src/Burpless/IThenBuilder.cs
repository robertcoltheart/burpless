using System;
using System.Linq.Expressions;

namespace Burpless;

public interface IThenBuilder<TContext> : IScenarioExecutor<TContext>
    where TContext : class
{
    IThenContinuationBuilder<TContext> Then(Expression<Action<TContext>> action);

    IThenContinuationBuilder<TContext> Then(Expression<Action<TContext, StepResult>> action);

    IThenContinuationBuilder<TContext> Then(Expression<Func<TContext, Task>> action);

    IThenContinuationBuilder<TContext> Then(Expression<Func<TContext, StepResult, Task>> action);

    IThenContinuationBuilder<TContext> Then(string step, Action<TContext> action);

    IThenContinuationBuilder<TContext> Then(string step, Action<TContext, StepResult> action);

    IThenContinuationBuilder<TContext> Then(string step, Func<TContext, Task> action);

    IThenContinuationBuilder<TContext> Then(string step, Func<TContext, StepResult, Task> action);
}

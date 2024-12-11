using System.Linq.Expressions;

namespace Burpless.Builders;

public interface IThenContinuationBuilder<TContext> : IThenBuilder<TContext>
    where TContext : class
{
    IThenContinuationBuilder<TContext> And(Expression<Action<TContext>> action);

    IThenContinuationBuilder<TContext> And(Expression<Action<TContext, StepResult>> action);

    IThenContinuationBuilder<TContext> And(Expression<Func<TContext, Task>> action);

    IThenContinuationBuilder<TContext> And(Expression<Func<TContext, StepResult, Task>> action);

    IThenContinuationBuilder<TContext> And(string step, Action<TContext> action);

    IThenContinuationBuilder<TContext> And(string step, Action<TContext, StepResult> action);

    IThenContinuationBuilder<TContext> And(string step, Func<TContext, Task> action);

    IThenContinuationBuilder<TContext> And(string step, Func<TContext, StepResult, Task> action);

    IThenContinuationBuilder<TContext> But(Expression<Action<TContext>> action);

    IThenContinuationBuilder<TContext> But(Expression<Action<TContext, StepResult>> action);

    IThenContinuationBuilder<TContext> But(Expression<Func<TContext, Task>> action);

    IThenContinuationBuilder<TContext> But(Expression<Func<TContext, StepResult, Task>> action);

    IThenContinuationBuilder<TContext> But(string step, Action<TContext> action);

    IThenContinuationBuilder<TContext> But(string step, Action<TContext, StepResult> action);

    IThenContinuationBuilder<TContext> But(string step, Func<TContext, Task> action);

    IThenContinuationBuilder<TContext> But(string step, Func<TContext, StepResult, Task> action);
}

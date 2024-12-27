using System.Runtime.CompilerServices;

namespace Burpless;

public interface IScenarioExecutor<TContext>
    where TContext : class
{
    void Execute();

    Task ExecuteAsync();

    TaskAwaiter GetAwaiter();
}

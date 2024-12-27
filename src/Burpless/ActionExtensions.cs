namespace Burpless;

internal static class ActionExtensions
{
    public static Func<T1, T2, Task> ToAsync<T1, T2>(this Action<T1, T2> action)
    {
        return (param1, param2) =>
        {
            action(param1, param2);

            return Task.CompletedTask;
        };
    }

    public static Func<T, Task> ToAsync<T>(this Action<T> action)
    {
        return param =>
        {
            action(param);

            return Task.CompletedTask;
        };
    }
}

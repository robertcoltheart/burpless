namespace Burpless;

public static class TableExtensions
{
    public static T Get<T>(this Table table)
    {
        return default(T);
    }

    public static IEnumerable<T> GetAll<T>(this Table table)
    {
        return null;
    }

    public static void ShouldEqual<T>(this Table table, T value)
    {
    }

    public static void ShouldEqual<T>(this Table table, IEnumerable<T> values)
    {
    }
}

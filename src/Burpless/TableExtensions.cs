using Burpless.Tables;

namespace Burpless;

public static class TableExtensions
{
    private static readonly TableSerializer serializer = new();

    public static T Get<T>(this Table table)
        where T : new()
    {
        return table.GetAll<T>().First();
    }

    public static IEnumerable<T> GetAll<T>(this Table table)
        where T : new()
    {
        return serializer.Deserialize<T>(table);
    }

    public static void ShouldEqual<T>(this Table table, T value)
    {
    }

    public static void ShouldEqual<T>(this Table table, IEnumerable<T> values)
    {
    }
}

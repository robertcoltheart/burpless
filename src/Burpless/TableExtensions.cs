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

    public static bool AreEqual<T>(this Table table, params IEnumerable<T> values)
    {
        var differences = GetDifferences(table, values);

        return differences.All(x => x.Type == ComparisonType.Match);
    }

    public static void ShouldEqual<T>(this Table table, IEnumerable<T> values)
    {
        var differences = GetDifferences(table, values)
            .ToArray();

        if (differences.All(x => x.Type == ComparisonType.Match))
        {
            return;
        }

        var results = new DifferenceBuilder()
            .AppendTableHeaders(table.Columns);

        foreach (var difference in differences)
        {
            difference.Format(results);
        }

        throw new TableValidationException($"Mismatched table with data:{Environment.NewLine}{results}");
    }

    private static IEnumerable<IComparison> GetDifferences<T>(Table table, IEnumerable<T> values)
    {
        var comparer = new TableComparer<T>(table, values.ToArray());

        return comparer.Compare().ToArray();
    }
}

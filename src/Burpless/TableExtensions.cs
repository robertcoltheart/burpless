using Burpless.Tables;
using Burpless.Tables.Comparison;

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

        VerifyColumns(differences);
        VerifyRows(table, differences);
    }

    private static IEnumerable<IComparison> GetDifferences<T>(Table table, IEnumerable<T> values)
    {
        var comparer = new TableComparer<T>();

        return comparer.Compare(table, values.ToArray()).ToArray();
    }

    private static void VerifyColumns(IComparison[] differences)
    {
        var columnDifferences = differences
            .Where(x => x.Element == ElementType.Column)
            .ToArray();

        if (columnDifferences.Any())
        {
            var results = new ComparisonBuilder()
                .AppendTableHeaders("Missing");

            foreach (var difference in columnDifferences)
            {
                difference.Format(results);
            }

            throw new TableValidationException($"Missing properties in data:{Environment.NewLine}{results}");
        }
    }

    private static void VerifyRows(Table table, IComparison[] differences)
    {
        var rowDifferences = differences
            .Where(x => x.Element == ElementType.Row)
            .ToArray();

        if (rowDifferences.Any())
        {
            var results = new ComparisonBuilder()
                .AppendTableHeaders(table.Columns);

            foreach (var difference in differences)
            {
                difference.Format(results);
            }

            throw new TableValidationException($"Mismatched table with data:{Environment.NewLine}{results}");
        }
    }
}

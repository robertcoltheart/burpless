using Burpless.Tables;
using Burpless.Tables.Comparison;

namespace Burpless;

/// <summary>
/// Provides extensions methods for <see cref="Table" /> objects.
/// </summary>
public static class TableExtensions
{
    private static readonly TableSerializer serializer = new();

    /// <summary>
    /// Parses the first row of a table to the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the elements to parse the first table row to.</typeparam>
    /// <param name="table">The <see cref="Table" /> that contains rows.</param>
    /// <returns>The first row in the table as the specified type.</returns>
    /// <exception cref="InvalidOperationException">The table has no rows.</exception>
    public static T Get<T>(this Table table)
        where T : new()
    {
        return table.GetAll<T>().First();
    }

    /// <summary>
    /// Parses all the rows of a table to the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the elements to parse the table rows to.</typeparam>
    /// <param name="table">The <see cref="Table" /> that contains rows.</param>
    /// <returns>The rows of the table as the specified type.</returns>
    public static IEnumerable<T> GetAll<T>(this Table table)
        where T : new()
    {
        return serializer.Deserialize<T>(table);
    }

    /// <summary>
    /// Determines whether a table is equal to the provided collection of objects.
    /// </summary>
    /// <typeparam name="T">The type of the elements of <paramref name="values"/>.</typeparam>
    /// <param name="table">The <see cref="Table"/> that is used for comparison.</param>
    /// <param name="values">The collection of values that is used for comparison.</param>
    /// <returns><see langword="true" /> if the table and the collection are equal according to the default equality comparer for their type; otherwise, <see langword="false" />.</returns>
    public static bool AreEqual<T>(this Table table, params IEnumerable<T> values)
    {
        var differences = GetDifferences(table, values);

        return differences.All(x => x.Type == ComparisonType.Match);
    }

    /// <summary>
    /// Determines whether a table is equal to the provided collection of objects, and throws an exception if the collections are not equal.
    /// </summary>
    /// <typeparam name="T">The type of the elements of <paramref name="values"/>.</typeparam>
    /// <param name="table">The <see cref="Table"/> that is used for comparison.</param>
    /// <param name="values">The collection of values that is used for comparison.</param>
    /// <exception cref="TableValidationException">The values are not equal to the specified <see cref="Table"/>.</exception>
    public static void ShouldEqual<T>(this Table table, IEnumerable<T> values)
    {
        var differences = GetDifferences(table, values)
            .ToArray();

        VerifyColumns(typeof(T), differences);
        VerifyRows(table, differences);
    }

    private static IEnumerable<IComparison> GetDifferences<T>(Table table, IEnumerable<T> values)
    {
        var comparer = new TableComparer<T>();

        return comparer.Compare(table, values.ToArray()).ToArray();
    }

    private static void VerifyColumns(Type type, IComparison[] differences)
    {
        var columnDifferences = differences
            .Where(x => x.Element == ElementType.Column && x.Type != ComparisonType.Match)
            .ToArray();

        if (columnDifferences.Any())
        {
            var results = new ComparisonBuilder()
                .AppendTableHeaders($"<{type}>");

            foreach (var difference in columnDifferences)
            {
                difference.Format(results);
            }

            throw new TableValidationException($"Missing columns in data type:{Environment.NewLine}{results}");
        }
    }

    private static void VerifyRows(Table table, IComparison[] differences)
    {
        var rowDifferences = differences
            .Where(x => x.Element == ElementType.Row)
            .ToArray();

        if (rowDifferences.Any(x => x.Type != ComparisonType.Match))
        {
            var results = new ComparisonBuilder()
                .AppendTableHeaders(table.Columns);

            foreach (var difference in differences)
            {
                difference.Format(results);
            }

            throw new TableValidationException($"Mismatched table data:{Environment.NewLine}{results}");
        }
    }
}

using System.Reflection;

namespace Burpless.Tables;

internal class DifferenceComparer<T> : ITableComparer<T>
{
    private readonly Dictionary<string, PropertyInfo> properties = typeof(T).GetProperties()
        .ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

    public IEnumerable<IComparison> GetComparisons(Table table, T[] items)
    {
        var comparer = new RowComparer<T>(table.Columns);

        return GetComparisons(table, items, comparer);
    }

    private IEnumerable<IComparison> GetComparisons(Table table, T[] items, RowComparer<T> comparer)
    {
        var remaining = items.ToList();

        for (var i = 0; i < table.Rows.Count; i++)
        {
            var index = GetIndexOf(items, table, i, comparer);

            if (index < 0)
            {
                yield return new MatchedRowComparison(ComparisonType.Missing, table.Rows[i]);
            }
            else
            {
                remaining.RemoveAt(index);

                yield return new MatchedRowComparison(ComparisonType.Match, table.Rows[i]);
            }
        }

        foreach (var item in remaining)
        {
            var row = table.Columns
                .Select(x => properties[x.GetColumnName()])
                .Select(x => x.GetValue(item)?.ToString())
                .Select(x => x ?? string.Empty)
                .ToArray();

            yield return new MatchedRowComparison(ComparisonType.Additional, row);
        }
    }

    private int GetIndexOf(T[] items, Table table, int index, RowComparer<T> comparer)
    {
        for (var i = 0; i < items.Length; i++)
        {
            if (comparer.Equivalent(items[i], table.Rows[index]))
            {
                return i;
            }
        }

        return -1;
    }
}

using System.Reflection;

namespace Burpless.Tables.Comparison;

internal class TableDataComparer<T> : IComparer<Table, T[]>
{
    private readonly Dictionary<string, PropertyInfo> properties = typeof(T).GetProperties()
        .ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

    public IEnumerable<IComparison> Compare(Table table, T[] items)
    {
        var remaining = items.ToList();

        for (var i = 0; i < table.Rows.Count; i++)
        {
            var index = GetIndexOf(remaining, table, i);

            if (index < 0)
            {
                yield return new RowComparison(ComparisonType.Missing, table.Rows[i]);
            }
            else
            {
                remaining.RemoveAt(index);

                yield return new RowComparison(ComparisonType.Match, table.Rows[i]);
            }
        }

        foreach (var item in remaining)
        {
            var row = table.Columns
                .Select(x => properties[x.GetColumnName()])
                .Select(x => x.GetValue(item)?.ToString())
                .Select(x => x ?? string.Empty)
                .ToArray();

            yield return new RowComparison(ComparisonType.Additional, row);
        }
    }

    private int GetIndexOf(IList<T> items, Table table, int index)
    {
        for (var i = 0; i < items.Count; i++)
        {
            if (Equivalent(items[i], table.Columns, table.Rows[index]))
            {
                return i;
            }
        }

        return -1;
    }

    private bool Equivalent(T item, IList<string> columns, string?[] row)
    {
        for (var i = 0; i < columns.Count; i++)
        {
            var column = columns[i].GetColumnName();
            var property = properties[column];

            if (!TypeParser.TryParse(property.PropertyType, row[i], out var rowValue))
            {
                throw new FormatException($"Cannot parse value '{row[i]}' in '{column}' to type {property.PropertyType}");
            }

            var value = property.GetValue(item);

            if (!Equals(value, rowValue))
            {
                return false;
            }
        }

        return true;
    }
}

﻿using System.Reflection;

namespace Burpless.Tables.Comparison;

internal class TypePropertiesComparer<T> : IComparer<Table, T[]>
{
    private readonly Dictionary<string, PropertyInfo> properties = typeof(T).GetProperties()
        .ToDictionary(x => x.Name);

    public IEnumerable<IComparison> Compare(Table table, T[] items)
    {
        var columns = table.Columns
            .Select(x => x.GetColumnName());

        var missing = columns
            .Except(properties.Keys, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        foreach (var column in missing)
        {
            yield return new MissingPropertyComparison(ComparisonType.Missing, column);
        }
    }
}

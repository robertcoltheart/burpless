namespace Burpless.Tables.Validation;

internal class RowComparer<T>(IList<string> columns)
{
    public bool Equivalent(T item, string?[] row)
    {
        var properties = typeof(T).GetProperties()
            .ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

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

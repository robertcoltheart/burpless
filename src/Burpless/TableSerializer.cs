using System.Collections.Concurrent;
using System.Reflection;

namespace Burpless;

internal class TableSerializer
{
    private readonly ConcurrentDictionary<Type, Dictionary<string, PropertyInfo>> Properties = new();

    private readonly TableColumnFormatter formatter = new();

    public IEnumerable<T> Deserialize<T>(Table table)
        where T : new()
    {
        return table.Rows.Select(x => Deserialize<T>(table.Columns, x));
    }

    private T Deserialize<T>(IList<string> columns, string?[] row)
        where T : new()
    {
        var item = new T();

        var properties = GetProperties(typeof(T));

        var columnNames = columns
            .Select(x => formatter.GetPropertyName(x))
            .ToArray();

        for (var i = 0; i < columnNames.Length; i++)
        {
            var propertyName = columnNames[i];
            var property = properties.GetValueOrDefault(propertyName);

            if (property != null)
            {
                if (property.PropertyType == typeof(string))
                {
                    property.SetValue(item, row[i]);
                }
                else if (TypeParser.TryParse(property.PropertyType, row[i], out var value))
                {
                    property.SetValue(item, value);
                }
            }
        }

        return item;
    }

    private Dictionary<string, PropertyInfo> GetProperties(Type type)
    {
        return Properties.GetOrAdd(type, x => x.GetProperties().ToDictionary(y => y.Name, StringComparer.OrdinalIgnoreCase));
    }
}

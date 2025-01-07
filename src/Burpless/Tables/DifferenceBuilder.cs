using System.Text;

namespace Burpless.Tables;

internal class DifferenceBuilder
{
    private IList<string> columnHeaders = [];

    private readonly List<Difference> differences = new();

    public DifferenceBuilder AppendTableHeaders(IList<string> headers)
    {
        columnHeaders = headers;

        return this;
    }

    public DifferenceBuilder AppendDifference(ComparisonType type, string column)
    {
        differences.Add(new Difference(type, [column]));

        return this;
    }

    public DifferenceBuilder AppendDifference(ComparisonType type, string[] values)
    {
        differences.Add(new Difference(type, values));

        return this;
    }

    public override string ToString()
    {
        var widths = GetColumnWidths();

        return new StringBuilder()
            .Append(GetColumnHeaders(widths))
            .Append(GetRows(widths))
            .ToString();
    }

    private string GetColumnHeaders(int[] columnWidths)
    {
        var results = new StringBuilder();

        results.Append("  |");

        for (var i = 0; i < columnHeaders.Count; i++)
        {
            var column = columnHeaders[i];

            results.Append($" {column.PadLeft(columnWidths[i])} |");
        }

        return results.ToString();
    }

    private string GetRows(int[] columnWidths)
    {
        var results = new StringBuilder();

        foreach (var difference in differences)
        {
            if (difference.Type == ComparisonType.Missing)
            {
                results.Append("- |");
            }
            else if (difference.Type == ComparisonType.Additional)
            {
                results.Append("+ |");
            }
            else
            {
                results.Append("  |");
            }

            for (var i = 0; i < difference.Values.Length; i++)
            {
                results.Append($" {difference.Values[i].PadLeft(columnWidths[i])} |");
            }

            results.AppendLine();
        }

        return results.ToString();
    }

    private int[] GetColumnWidths()
    {
        var widths = columnHeaders
            .Select(x => x.Length)
            .ToArray();

        foreach (var row in differences)
        {
            for (var i = 0; i < row.Values.Length; i++)
            {
                widths[i] = Math.Max(widths[i], row.Values[i].Length);
            }
        }

        return widths;
    }

    private record Difference(ComparisonType Type, string[] Values);
}

using System.Text;

namespace Burpless.Tables.Validation;

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

        var results = new StringBuilder();
        AddColumnHeaders(results, widths);
        AddRows(results, widths);

        return results.ToString().TrimEnd();
    }

    private void AddColumnHeaders(StringBuilder results, int[] columnWidths)
    {
        results.Append("  |");

        for (var i = 0; i < columnHeaders.Count; i++)
        {
            var column = columnHeaders[i];

            results.Append($" {column.PadLeft(columnWidths[i])} |");
        }

        results.AppendLine();
    }

    private void AddRows(StringBuilder results, int[] columnWidths)
    {
        foreach (var difference in differences)
        {
            var prefix = difference.Type switch
            {
                ComparisonType.Missing => "- |",
                ComparisonType.Additional => "+ |",
                _ => "  |"
            };

            results.Append(prefix);

            for (var i = 0; i < difference.Values.Length; i++)
            {
                results.Append($" {difference.Values[i].PadLeft(columnWidths[i])} |");
            }

            results.AppendLine();
        }
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

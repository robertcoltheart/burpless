﻿using System.Text;

namespace Burpless.Tables.Comparison;

internal class ComparisonBuilder
{
    private IReadOnlyList<string> columnHeaders = [];

    private readonly List<Difference> differences = new();

    public ComparisonBuilder AppendTableHeaders(params IEnumerable<string> headers)
    {
        columnHeaders = headers.ToList();

        return this;
    }

    public ComparisonBuilder AppendColumnDifference(ComparisonType type, params IEnumerable<string> values)
    {
        differences.Add(new Difference(type, ElementType.Column, values.ToArray()));

        return this;
    }

    public ComparisonBuilder AppendRowDifference(ComparisonType type, params IEnumerable<string> values)
    {
        differences.Add(new Difference(type, ElementType.Row, values.ToArray()));

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
        if (columnHeaders.Count == 0)
        {
            return;
        }

        results.Append("  |");

        for (var i = 0; i < columnHeaders.Count; i++)
        {
            var column = columnHeaders[i];

            results.Append($" {column.PadRight(columnWidths[i])} |");
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
                results.Append($" {difference.Values[i].PadRight(columnWidths[i])} |");
            }

            results.AppendLine();
        }
    }

    private int[] GetColumnWidths()
    {
        var maxRowLength = differences.Any()
            ? differences.Max(x => x.Values.Length)
            : 0;

        var maxWidth = Math.Max(columnHeaders.Count, maxRowLength);

        var widths = new int[maxWidth];

        for (var i = 0; i < columnHeaders.Count; i++)
        {
            widths[i] = columnHeaders[i].Length;
        }

        foreach (var row in differences)
        {
            for (var i = 0; i < row.Values.Length; i++)
            {
                widths[i] = Math.Max(widths[i], row.Values[i].Length);
            }
        }

        return widths;
    }

    private record Difference(ComparisonType Type, ElementType Element, string[] Values);
}

using System.Text;

namespace Burpless;

internal class TableParser
{
    public Table Parse(string value)
    {
        using var reader = new StringReader(value.Trim());

        var row = ReadCells(reader.ReadLine()).ToArray();

        if (!row.Any())
        {
            throw new ArgumentException("Expected table header row");
        }

        var table = Table.WithColumns(row);

        while (row.Any())
        {
            if (IsEndOfReader(reader))
            {
                break;
            }

            row = ReadCells(reader.ReadLine()).ToArray();

            if (row.Length != table.Columns.Count)
            {
                throw new ArgumentException($"Expected {table.Columns.Count} cells in row but got {row.Length}");
            }

            table.AddRow(row);
        }

        return table;
    }

    private IEnumerable<string> ReadCells(string? value)
    {
        if (value == null)
        {
            yield break;
        }

        using var reader = new StringReader(value.Trim());

        if (reader.Peek() != '|')
        {
            throw new ArgumentException("Table row must start with '|'");
        }

        reader.Read();

        var cell = ReadCell(reader);

        while (cell != null)
        {
            yield return cell;

            cell = ReadCell(reader);
        }
    }

    private string? ReadCell(StringReader reader)
    {
        var buffer = new StringBuilder();

        while (!IsEndOfReader(reader))
        {
            if (reader.Peek() == '|')
            {
                reader.Read();

                return buffer.ToString().Trim();
            }

            if (reader.Peek() == '\\')
            {
                reader.Read();

                if (reader.Peek() == 'n')
                {
                    buffer.Append('\n');
                }
                else if (reader.Peek() != '|' && reader.Peek() != '\\')
                {
                    buffer.Append('\\');
                    buffer.Append((char)reader.Peek());
                }
            }
            else
            {
                buffer.Append((char)reader.Peek());
            }

            reader.Read();
        }

        return null;
    }

    private bool IsEndOfReader(StringReader reader)
    {
        return reader.Peek() == -1;
    }
}

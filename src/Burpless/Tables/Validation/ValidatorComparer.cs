using Burpless.Tables.Comparison;

namespace Burpless.Tables.Validation;

internal class ValidatorComparer<T> : IComparer<Table, T[]>
{
    public IEnumerable<IComparison> Compare(Table table, T[] items)
    {
        if (table.Validator == null)
        {
            yield break;
        }

        foreach (var item in items)
        {
            var row = new string[table.Columns.Count];
            var valid = true;

            for (var i = 0; i < table.Columns.Count; i++)
            {
                var isValid = table.Validator.IsValid(table.Columns[i], item!, out var value);

                if (!isValid)
                {
                    value = $"**{value}";
                    valid = false;
                }

                row[i] = value;
            }

            if (valid)
            {
                yield return new RowComparison(ComparisonType.Match, row.ToArray());
            }
            else
            {
                yield return new RowComparison(ComparisonType.Additional, row.ToArray());
            }
        }
    }
}

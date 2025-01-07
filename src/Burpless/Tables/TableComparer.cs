using Burpless.Tables.Comparison;

namespace Burpless.Tables;

internal class TableComparer<T> : IComparer<Table, T[]>
{
    private static readonly IComparer<Table, T[]>[] Comparers =
    [
        new TypePropertiesComparer<T>(),
        new TableDataComparer<T>()
    ];

    public IEnumerable<IComparison> Compare(Table table, T[] items)
    {
        return Comparers.SelectMany(comparer => comparer.Compare(table, items));
    }
}

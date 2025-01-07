namespace Burpless.Tables;

internal class TableComparer<T>(Table table, T[] items)
{
    private static readonly ITableComparer<T>[] Comparers =
    [
        new TypePropertiesComparer<T>(),
        new DifferenceComparer<T>()
    ];

    public IEnumerable<IComparison> Compare()
    {
        return Comparers.SelectMany(comparer => comparer.GetComparisons(table, items));
    }
}

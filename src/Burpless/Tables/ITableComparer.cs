namespace Burpless.Tables;

internal interface ITableComparer<in T>
{
    IEnumerable<IComparison> GetComparisons(Table table, T[] items);
}

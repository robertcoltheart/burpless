namespace Burpless.Tables;

internal interface ITableComparer<in T>
{
    IEnumerable<IComparison> Equals(Table table, T[] items);
}

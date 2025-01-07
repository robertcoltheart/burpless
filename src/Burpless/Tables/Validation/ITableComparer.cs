namespace Burpless.Tables.Validation;

internal interface ITableComparer<in T>
{
    IEnumerable<IComparison> GetComparisons(Table table, T[] items);
}

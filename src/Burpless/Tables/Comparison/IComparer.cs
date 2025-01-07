namespace Burpless.Tables.Comparison;

internal interface IComparer<in TSelf, in TOther>
{
    IEnumerable<IComparison> Compare(TSelf table, TOther items);
}

using Burpless.Tables.Comparison;

namespace Burpless.Tables;

internal interface IComparer<in TSelf, in TOther>
{
    IEnumerable<IComparison> Compare(TSelf table, TOther items);
}

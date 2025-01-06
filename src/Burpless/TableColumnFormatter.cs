namespace Burpless;

internal class TableColumnFormatter
{
    public string GetPropertyName(string column)
    {
        return column
            .Replace(" ", string.Empty)
            .Replace("-", string.Empty)
            .Replace("_", string.Empty);
    }
}

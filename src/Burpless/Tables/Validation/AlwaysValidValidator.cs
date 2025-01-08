namespace Burpless.Tables.Validation;

internal class AlwaysValidValidator : ITableValidatorExecutor
{
    public bool IsValid(string column, object item, out string value)
    {
        value = string.Empty;

        return true;
    }
}

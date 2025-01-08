namespace Burpless.Tables.Validation;

internal interface ITableValidatorExecutor
{
    bool IsValid(string column, object item, out string value);
}

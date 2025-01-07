namespace Burpless.Tables.Validation;

internal interface ITableValidatorExecutor
{
    Type Type { get; }

    bool IsValid(params object[] values);

    void Validate(params object[] values);
}

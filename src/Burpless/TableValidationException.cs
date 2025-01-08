namespace Burpless;

public class TableValidationException : Exception
{
    public TableValidationException()
    {
    }

    public TableValidationException(string message)
        : base(message)
    {
    }

    public TableValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

namespace Burpless;

/// <summary>
/// The exception that is thrown when data does not match the specified table.
/// </summary>
public class TableValidationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TableValidationException" /> class.
    /// </summary>
    public TableValidationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TableValidationException" /> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public TableValidationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TableValidationException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public TableValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

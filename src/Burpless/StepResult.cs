namespace Burpless;

public class StepResult
{
    public bool IsSuccess { get; private set; }

    public Exception? Exception { get; private set; }

    public static StepResult Success()
    {
        return new StepResult
        {
            IsSuccess = true
        };
    }

    public static StepResult Failed(Exception exception)
    {
        return new StepResult
        {
            IsSuccess = false,
            Exception = exception
        };
    }
}

namespace Burpless.Tables.Comparison;

internal class ObjectComparer
{
    public bool Equal(object? expected, object? actual)
    {
        var expectedString = expected as string;
        var actualString = actual as string;

        if (string.IsNullOrEmpty(expectedString) && string.IsNullOrEmpty(actualString))
        {
            return true;
        }

        return Equals(expected, actual);
    }
}

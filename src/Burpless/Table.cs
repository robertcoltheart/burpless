namespace Burpless;

public class Table
{
    public static implicit operator Table(string value)
    {
        return null;
    }

    public T Get<T>()
    {
        return default;
    }

    public IEnumerable<T> GetAll<T>()
    {
        return null;
    }
}

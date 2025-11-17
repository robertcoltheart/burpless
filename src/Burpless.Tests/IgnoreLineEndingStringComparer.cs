namespace Burpless.Tests;

public class IgnoreLineEndingStringComparer : StringComparer
{
    public static IgnoreLineEndingStringComparer Instance { get; } = new();

    public override int Compare(string? x, string? y)
    {
        var normalizedX = x?.ReplaceLineEndings();
        var normalizedY = y?.ReplaceLineEndings();

        return StringComparer.Ordinal.Compare(normalizedX, normalizedY);
    }

    public override bool Equals(string? x, string? y)
    {
        var normalizedX = x?.ReplaceLineEndings();
        var normalizedY = y?.ReplaceLineEndings();

        return StringComparer.Ordinal.Equals(normalizedX, normalizedY);
    }

    public override int GetHashCode(string obj)
    {
        if (obj == null)
        {
            return 0;
        }

        var normalizedObj = obj.ReplaceLineEndings();

        return StringComparer.Ordinal.GetHashCode(normalizedObj);
    }

    private string NormalizeLineEndings(string value)
    {
        return value.ReplaceLineEndings();
    }
}

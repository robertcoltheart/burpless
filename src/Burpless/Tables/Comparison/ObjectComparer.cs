namespace Burpless.Tables.Comparison;

internal class ObjectComparer
{
    public bool Equal(Type propertyType, object? expected, object? actual)
    {
        if (GetPropertyType(propertyType) == typeof(string))
        {
            var expectedString = expected as string;
            var actualString = actual as string;

            if (string.IsNullOrEmpty(expectedString) && string.IsNullOrEmpty(actualString))
            {
                return true;
            }
        }

        return Equals(expected, actual);
    }

    private Type GetPropertyType(Type propertyType)
    {
        var nullableType = Nullable.GetUnderlyingType(propertyType);

        return nullableType ?? propertyType;
    }
}

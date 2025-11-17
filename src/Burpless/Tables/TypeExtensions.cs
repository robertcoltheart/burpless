using System.Collections.Frozen;

namespace Burpless.Tables;

internal static class TypeExtensions
{
    private static readonly FrozenSet<Type> CollectionTypes = FrozenSet.ToFrozenSet([
        typeof(List<>),
        typeof(ICollection<>),
        typeof(IEnumerable<>),
        typeof(IList<>),
        typeof(IReadOnlyCollection<>),
        typeof(IReadOnlyList<>)]);

    public static bool IsCollection(this Type type)
    {
        if (!type.IsGenericType)
        {
            return false;
        }

        var typeDefinition = type.GetGenericTypeDefinition();

        return CollectionTypes.Contains(typeDefinition);
    }

    public static object? GetDefaultValue(this Type type)
    {
        if (type.IsValueType)
        {
            return Activator.CreateInstance(type);
        }

        return null;
    }
}

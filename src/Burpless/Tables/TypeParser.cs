using System.Collections;
using System.Collections.Concurrent;
using System.Globalization;
using System.Numerics;
using System.Reflection;

namespace Burpless.Tables;

internal static class TypeParser
{
    private static readonly ConcurrentDictionary<Type, MethodInfo?> ParseMethods = new();

    private static readonly ConcurrentDictionary<Type, MethodInfo?> CustomParseMethods = new();

    private static readonly HashSet<Type> ParseableTypes =
    [
        typeof(bool),
        typeof(byte),
        typeof(sbyte),
        typeof(char),
        typeof(DateTime),
        typeof(DateOnly),
        typeof(TimeOnly),
        typeof(DateTimeOffset),
        typeof(TimeSpan),
        typeof(Half),
        typeof(float),
        typeof(double),
        typeof(decimal),
        typeof(Guid),
        typeof(short),
        typeof(ushort),
        typeof(int),
        typeof(uint),
        typeof(long),
        typeof(ulong),
        typeof(Int128),
        typeof(UInt128),
        typeof(BigInteger),
        typeof(Complex),
        typeof(string),
        typeof(Uri)
    ];

    private static readonly char[] Separators = [',', ';'];

    public static bool TryParse(Type type, string? value, out object parsed)
    {
        type = GetType(type);

        if (BurplessSettings.Instance.CustomParsers.ContainsKey(type))
        {
            if (TryParseCustom(type, value, out parsed))
            {
                return true;
            }
        }

        if (ParseableTypes.Contains(type))
        {
            return TryParseExact(type, value, out parsed);
        }

        if (type.IsArray)
        {
            return TryParseArray(type, value, out parsed);
        }

        if (type.IsCollection())
        {
            return TryParseList(type, value, out parsed);
        }

        if (type.IsEnum)
        {
            if (Enum.TryParse(type, value, true, out parsed!))
            {
                return true;
            }
        }

        var isParsable = type
            .GetInterfaces()
            .Any(c => c.IsGenericType && c.GetGenericTypeDefinition() == typeof(IParsable<>));

        if (isParsable)
        {
            var parse = GetParseMethod(type);

            if (parse != null)
            {
                try
                {
                    parsed = parse.Invoke(null, [value, CultureInfo.CurrentCulture])!;

                    return true;
                }
                catch
                {
                    parsed = null!;
                }
            }
        }

        parsed = null!;

        return false;
    }

    private static Type GetType(Type type)
    {
        var underlyingType = Nullable.GetUnderlyingType(type);

        if (underlyingType != null)
        {
            return underlyingType;
        }

        return type;
    }

    private static MethodInfo? GetParseMethod(Type type)
    {
        return ParseMethods.GetOrAdd(type, _ =>
        {
            var isParsable = type.GetInterfaces()
                .Any(c => c.IsGenericType && c.GetGenericTypeDefinition() == typeof(IParsable<>));

            return isParsable
                ? type.GetMethod("Parse", [typeof(string), typeof(IFormatProvider)])
                : null;
        });
    }

    private static bool TryParseExact(Type type, string? value, out object parsed)
    {
        return type switch
        {
            _ when type == typeof(bool) => TryParse<bool>(value, out parsed),
            _ when type == typeof(byte) => TryParse<byte>(value, out parsed),
            _ when type == typeof(sbyte) => TryParse<sbyte>(value, out parsed),
            _ when type == typeof(char) => TryParse<char>(value, out parsed),
            _ when type == typeof(DateTime) => TryParse<DateTime>(value, out parsed),
            _ when type == typeof(DateOnly) => TryParse<DateOnly>(value, out parsed),
            _ when type == typeof(TimeOnly) => TryParse<TimeOnly>(value, out parsed),
            _ when type == typeof(DateTimeOffset) => TryParse<DateTimeOffset>(value, out parsed),
            _ when type == typeof(TimeSpan) => TryParse<TimeSpan>(value, out parsed),
            _ when type == typeof(Half) => TryParse<Half>(value, out parsed),
            _ when type == typeof(float) => TryParse<float>(value, out parsed),
            _ when type == typeof(double) => TryParse<double>(value, out parsed),
            _ when type == typeof(decimal) => TryParse<decimal>(value, out parsed),
            _ when type == typeof(Guid) => TryParse<Guid>(value, out parsed),
            _ when type == typeof(short) => TryParse<short>(value, out parsed),
            _ when type == typeof(ushort) => TryParse<ushort>(value, out parsed),
            _ when type == typeof(int) => TryParse<int>(value, out parsed),
            _ when type == typeof(uint) => TryParse<uint>(value, out parsed),
            _ when type == typeof(long) => TryParse<long>(value, out parsed),
            _ when type == typeof(ulong) => TryParse<ulong>(value, out parsed),
            _ when type == typeof(Int128) => TryParse<Int128>(value, out parsed),
            _ when type == typeof(UInt128) => TryParse<UInt128>(value, out parsed),
            _ when type == typeof(BigInteger) => TryParse<BigInteger>(value, out parsed),
            _ when type == typeof(Complex) => TryParse<Complex>(value, out parsed),
            _ when type == typeof(string) => TryParse<string>(value, out parsed),
            _ when type == typeof(Uri) => TryParseUri(value, out parsed),
            _ => throw new ArgumentException($"Cannot parse type {type}")
        };
    }

    private static bool TryParseCustom(Type type, string? value, out object parsed)
    {
        var parser = BurplessSettings.Instance.CustomParsers[type];

        var method = CustomParseMethods.GetOrAdd(parser.GetType(),
            parserType => parserType.GetMethod("Parse", [typeof(string), typeof(IFormatProvider)]));

        try
        {
            parsed = method!.Invoke(null, [value, null])!;

            return true;
        }
        catch
        {
            parsed = null!;
        }

        return false;
    }

    private static bool TryParse<T>(string? value, out object parsed)
        where T : IParsable<T>
    {
        if (T.TryParse(value, CultureInfo.CurrentCulture, out var result))
        {
            parsed = result;

            return true;
        }

        parsed = null!;

        return false;
    }

    private static bool TryParseUri(string? value, out object parsed)
    {
        if (string.IsNullOrEmpty(value))
        {
            parsed = null!;

            return false;
        }

        try
        {
            parsed = new Uri(value);

            return true;
        }
        catch 
        {
            parsed = null!;
        }

        return false;
    }

    private static bool TryParseArray(Type type, string? value, out object parsed)
    {
        var valueType = type.GetElementType();

        if (valueType == null)
        {
            parsed = null!;

            return false;
        }

        if (string.IsNullOrEmpty(value))
        {
            parsed = Enumerable.Empty<object>();

            return true;
        }

        var values = value.Split(Separators);
        var items = GetEnumerableItems(values, valueType);

        var array = Array.CreateInstance(valueType, values.Length);
        var index = 0;

        foreach (var item in items)
        {
            array.SetValue(item, index++);
        }

        parsed = array;

        return true;
    }

    private static bool TryParseList(Type type, string? value, out object parsed)
    {
        var valueType = type.GetGenericArguments().First();

        if (string.IsNullOrEmpty(value))
        {
            parsed = Enumerable.Empty<object>();

            return true;
        }

        var values = value.Split(Separators);
        var items = GetEnumerableItems(values, valueType);

        var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(valueType))!;

        foreach (var item in items)
        {
            list.Add(Convert.ChangeType(item, valueType));
        }

        parsed = list;

        return true;
    }

    private static IEnumerable GetEnumerableItems(string[] values, Type valueType)
    {
        foreach (var value in values)
        {
            if (TryParseExact(valueType, value.Trim(), out var parsedValue))
            {
                yield return parsedValue;
            }
            else
            {
                yield return Activator.CreateInstance(valueType);
            }
        }
    }
}

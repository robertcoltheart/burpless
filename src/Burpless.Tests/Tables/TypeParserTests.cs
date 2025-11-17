using Burpless.Tables;

namespace Burpless.Tests.Tables;

public class TypeParserTests
{
    [Test]
    [Arguments(typeof(string), "value")]
    [Arguments(typeof(int), "123")]
    [Arguments(typeof(decimal), "123.123")]
    [Arguments(typeof(DateOnly), "2024-12-25")]
    public async Task CanParseBasicTypes(Type type, string value)
    {
        var parsed = TypeParser.TryParse(type, value, out _);

        await Assert.That(parsed).IsTrue();
    }

    [Test]
    [Arguments(typeof(object), "value")]
    public async Task CannotParseUnsupportedTypes(Type type, string value)
    {
        var parsed = TypeParser.TryParse(type, value, out _);

        await Assert.That(parsed).IsFalse();
    }

    [Test]
    [Arguments("http://google.com", true)]
    [Arguments("invalid", false)]
    public async Task CanParseUri(string value, bool valid)
    {
        var parsed = TypeParser.TryParse(typeof(Uri), value, out var uri);

        await Assert.That(parsed).IsEqualTo(valid);

        if (valid)
        {
            await Assert.That(uri).IsTypeOf<Uri>().And.IsEqualTo(new Uri(value));
        }
    }

    [Test]
    public async Task CanParseStringArray()
    {
        var parsed = TypeParser.TryParse(typeof(string[]), "val1,val2", out var array);

        await Assert.That(parsed).IsTrue();
        await Assert.That(array).IsTypeOf<string[]>()
            .And.Contains("val1")
            .And.Contains("val2");
    }

    [Test]
    public async Task CanParseTypedArray()
    {
        var parsed = TypeParser.TryParse(typeof(int[]), "1, 2", out var array);

        await Assert.That(parsed).IsTrue();
        await Assert.That(array).IsTypeOf<int[]>()
            .And.Contains(1)
            .And.Contains(2);
    }

    [Test]
    public async Task InvalidValueInTypedArrayReturnsDefault()
    {
        var parsed = TypeParser.TryParse(typeof(int[]), "1, 2, wrong", out var array);

        await Assert.That(parsed).IsTrue();
        await Assert.That(array).IsTypeOf<int[]>().And.IsEquivalentTo([1, 2, 0]);
    }

    [Test]
    public async Task CanParseStringList()
    {
        var parsed = TypeParser.TryParse(typeof(List<string>), "val1,val2", out var list);

        await Assert.That(parsed).IsTrue();
        await Assert.That(list).IsTypeOf<List<string>>()
            .And.Contains("val1")
            .And.Contains("val2");
    }

    [Test]
    public async Task CanParseTypedList()
    {
        var parsed = TypeParser.TryParse(typeof(List<int>), "1, 2", out var list);

        await Assert.That(parsed).IsTrue();
        await Assert.That(list).IsTypeOf<List<int>>()
            .And.Contains(1)
            .And.Contains(2);
    }

    [Test]
    public async Task InvalidValueInTypedListReturnsDefault()
    {
        var parsed = TypeParser.TryParse(typeof(List<int>), "1, 2, wrong", out var array);

        await Assert.That(parsed).IsTrue();
        await Assert.That(array).IsTypeOf<List<int>>().And.IsEquivalentTo([1, 2, 0]);
    }

    [Test]
    public async Task CanParseCustomType()
    {
        try
        {
            BurplessSettings.Instance.CustomParsers[typeof(Value)] = new ValueParser();

            var result = TypeParser.TryParse(typeof(Value), "123", out var parsed);
            var value = (Value)parsed;

            await Assert.That(result).IsTrue();
            await Assert.That(value.Number).IsEqualTo(123);
        }
        finally
        {
            BurplessSettings.Instance.CustomParsers.TryRemove(typeof(Value), out _);
        }
    }

    private struct Value
    {
        public int Number;
    }

    private struct ValueParser : IParser<Value>
    {
        public static Value Parse(string value, IFormatProvider? provider)
        {
            var parsed = int.Parse(value);

            return new Value
            {
                Number = parsed
            };
        }
    }
}

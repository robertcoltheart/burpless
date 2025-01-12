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
    [Arguments(typeof(Uri), "value")]
    public async Task CannotParseUnsupportedTypes(Type type, string value)
    {
        var parsed = TypeParser.TryParse(type, value, out _);

        await Assert.That(parsed).IsFalse();
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

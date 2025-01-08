using Burpless.Tables.Comparison;

namespace Burpless.Tests.Tables.Comparison;

public class TypePropertiesComparerTests
{
    [Test]
    [Arguments(0, new[] { "StringValue", "int value", "nullable-int_value", "Dateonly Value" })]
    [Arguments(1, new[] { "StringValuess", "int value", "nullable-int_value", "Dateonly Value" })]
    [Arguments(2, new[] { "wrong", "int value", "wrong again", "Dateonly Value" })]
    [Arguments(2, new[] { "wrong", "int value", "wrong again" })]
    [Arguments(2, new[] { "wrong", "wrong again" })]
    [Arguments(0, new[] { "String value" })]
    [Arguments(0, new[] { "String value", "NullableIntValue" })]
    [Arguments(0, new string[0])]
    public async Task CanCompareTypePropertiesWithColumns(int differences, string[] columns)
    {
        var table = Table.WithColumns(columns);

        var comparer = new TypePropertiesComparer<PropertiesClass>();
        var result = comparer.Compare(table, []);

        await Assert.That(result).HasCount().EqualTo(differences);
    }

    private class PropertiesClass
    {
        public string StringValue { get; set; }

        public int IntValue { get; set; }

        public int? NullableIntValue { get; set; }

        public DateOnly DateOnlyValue { get; set; }
    }
}

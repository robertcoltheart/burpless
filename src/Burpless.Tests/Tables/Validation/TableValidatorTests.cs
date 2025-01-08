using Burpless.Tables.Validation;

namespace Burpless.Tests.Tables.Validation;

public class TableValidatorTests
{
    [Test]
    [Arguments(true, "IntValue", 1, "value")]
    [Arguments(true, "StringValue", 1, "value")]
    [Arguments(true, "wrong column", 1, "value")]
    [Arguments(false, "IntValue", 5, "value")]
    [Arguments(false, "StringValue", 1, "wrong value")]
    public async Task CanVerifyModelUsingTableValidator(bool equals, string column, int intValue, string stringValue)
    {
        var validator = new TableValidator<Model>();

        validator
            .WithColumn(x => x.IntValue, x => x == 1)
            .WithColumn(x => x.StringValue, x => x == "value");

        var model = new Model
        {
            IntValue = intValue,
            StringValue = stringValue
        };

        var result = validator.IsValid(column, model, out _);

        await Assert.That(result).IsEqualTo(equals);
    }

    [Test]
    public void WrongTypeToValidateThrows()
    {
        var validator = new TableValidator<Model>();

        validator
            .WithColumn(x => x.IntValue, x => x == 1)
            .WithColumn(x => x.StringValue, x => x == "value");

        var model = new Dictionary<string, string>();

        Assert.Throws(() => validator.IsValid("column", model, out _));
    }

    [Test]
    [Arguments("IntValue", "1")]
    [Arguments("StringValue", "value")]
    public async Task ValueIsParsedOnValidation(string column, string expected)
    {
        var validator = new TableValidator<Model>();

        validator
            .WithColumn(x => x.IntValue, x => x == 1)
            .WithColumn(x => x.StringValue, x => x == "value");

        var model = new Model
        {
            IntValue = 1,
            StringValue = "value"
        };

        validator.IsValid(column, model, out var parsed);

        await Assert.That(parsed).IsEqualTo(expected);
    }

    private class Model
    {
        public int IntValue { get; set; }

        public string StringValue { get; set; } = null!;
    }
}

namespace Burpless.Tests;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("MethodNameTurnedIntoSentence", "method name turned into sentence")]
    [InlineData("SomethingWith1stNumbering", "something with 1st numbering")]
    [InlineData("ANameStartingWithA", "a name starting with a")]
    [InlineData("WhenABigNumber1234IsInTheMiddle", "when a big number 1234 is in the middle")]
    [InlineData("When_a_method_is_underscored", "when a method is underscored")]
    [InlineData("With__multiple__underscores____here", "with multiple underscores here")]
    [InlineData("WhenIUseMyOwnName", "when I use my own name")]
    [InlineData("WithANumberAtTheEnd100", "with a number at the end 100")]
    [InlineData("WithTheUSGovernment", "with the US government")]
    [InlineData("AnABCAgreement", "an ABC agreement")]
    public void CanConvertMethodNameToSentence(string input, string expected)
    {
        Assert.Equal(expected, input.ToSentence());
    }
}

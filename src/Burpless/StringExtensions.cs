using System.Text.RegularExpressions;

namespace Burpless;

internal static class StringExtensions
{
    private const string Word = @"\p{Lu}?\p{Ll}+";

    private const string NumberFollowedByLetters = @"[0-9]+\p{Ll}*";

    private const string Acronym = @"\p{Lu}+(?=\p{Lu}|[0-9]|\b)";

    private const string OtherLetters = @"\p{Lo}+";

    private static readonly Regex PascalCaseWordPartsRegex = new(
        $"{Word}|{NumberFollowedByLetters}|{Acronym}|{OtherLetters}",
        RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture | RegexOptions.Compiled);

    public static string ToSentence(this string value)
    {
        if (value.All(char.IsUpper))
        {
            return value;
        }

        var words = PascalCaseWordPartsRegex.Matches(value)
            .OfType<Match>()
            .Select(x =>
            {
                var isUppercase = x.Value.All(char.IsUpper);

                var isPronoun = x.Value == "I";
                var isAcronym = isUppercase && x.Value.Length > 1;

                if (isPronoun || isAcronym)
                {
                    return x.Value;
                }

                return x.Value.ToLower();
            });

        return string.Join(" ", words);
    }

    public static string GetColumnName(this string value)
    {
        return value
            .Replace(" ", string.Empty)
            .Replace("-", string.Empty)
            .Replace("_", string.Empty);
    }
}

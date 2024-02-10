namespace EmergentDesign.Common;

public static class StringExtensions
{
    public static IEnumerable<IEnumerable<int>> NonNegativeIntSequences(this IEnumerable<string> lines)
    {
        return lines
            .Select(line => line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries))
            .Select(words => words.Select(word => (valid: int.TryParse(word, out var value) && value > 0, value)))
            .Where(matches => matches.AllNonEmpty(match => match.valid))
            .Select(matches => matches.Select(match => match.value));
    }
    // .Where(line => Regex.IsMatch(line, @"^[\s\t\d]+$"))
    // .Where(line => Regex
    //     .Matches(line, @"\d+")
    //     .All(match => int.TryParse(match.Value, out _)))
    // .Select(line => line.ToNonNegativeInts())
    // .Where(numbers => numbers.Any());

    public static IEnumerable<int> NonNegativeInt(this IEnumerable<string> lines)
    {
        return lines
            .Select(line => (valid: int.TryParse(line, out var value) && value > 0, value))
            .Where(match => match.valid)
            .Select(match => match.value);
    }
}
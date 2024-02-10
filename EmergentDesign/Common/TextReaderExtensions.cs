namespace EmergentDesign.Common;

public static class TextReaderExtensions
{
    public static IEnumerable<string> IncomingLines(this TextReader reader, Action prompt)
    {
        return reader.NullableIncomingLines(prompt).TakeWhile(line => !ReferenceEquals(line, null))!;
    }

    private static IEnumerable<string?> NullableIncomingLines(this TextReader reader, Action prompt)
    {
        while (true)
        {
            prompt();
            yield return reader.ReadLine();
        }
        // ReSharper disable once IteratorNeverReturns
    }
}
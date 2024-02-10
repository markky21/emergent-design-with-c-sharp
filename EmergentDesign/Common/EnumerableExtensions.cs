namespace EmergentDesign.Common;

public static class EnumerableExtensions
{
    public static void WriteLinesTo<T>(this IEnumerable<T> sequence, TextWriter destination)
    {
        sequence.Select(obj => $"{obj}").WriteLinesTo(destination);
    }

    public static void WriteLinesTo(this IEnumerable<string> lines, TextWriter destination)
    {
        foreach (var line in lines)
            destination.WriteLine(line);
    }

    public static string JoinToText(this IEnumerable<int> sequence, string separator)
    {
        return string.Join(separator, sequence);
    }

    public static bool AllNonEmpty<T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
    {
        var allMatch = false;

        foreach (var item in sequence)
        {
            if (!predicate(item)) return false;
            allMatch = true;
        }

        return allMatch;
    }

    public static bool IsEmpty<T>(this IEnumerable<T> sequence)
    {
        return !sequence.Any();
    }

    public static Partition<T> AsPartition<T>(this IEnumerable<T> sequence)
    {
        return new Partition<T>(sequence);
    }

    public static (IEnumerable<T> prefix, T last) ExtractLast<T>(this IEnumerable<T> sequence)
    {
        var prefix = new List<T>();
        using (var enumerator = sequence.GetEnumerator())
        {
            enumerator.MoveNext();
            var last = enumerator.Current;

            while (enumerator.MoveNext())
            {
                prefix.Add(last);
                last = enumerator.Current;
            }

            return (prefix, last);
        }
    }

    public static IEnumerable<IEnumerable<T>> CrossProduct<T>(this IEnumerable<IEnumerable<T>> sequenceOfSequences)
    {
        var data = sequenceOfSequences.Select(sequence => sequence.ToArray()).ToArray();
        var indices = new int[data.Length];
        var carryOver = 0;

        while (carryOver == 0)
        {
            yield return indices.Select((column, row) => data[row][column]).ToList();
            carryOver = 1;
            for (var row = 0; carryOver > 0 && row < indices.Length; row++)
            {
                indices[row] += 1;
                carryOver = indices[row] / data[row].Length;
                indices[row] = indices[row] % data[row].Length;
            }
        }
    }
}

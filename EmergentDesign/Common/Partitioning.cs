using System.Collections;

namespace EmergentDesign.Common;

internal class Partitioning<T> : IEnumerable<Partition<T>>
{
    public Partitioning(params Partition<T>[] partitions) : this((IEnumerable<Partition<T>>)partitions)
    {
    }

    public Partitioning(IEnumerable<Partition<T>> partitions)
    {
        Partitions = partitions.ToList();
    }

    private IEnumerable<Partition<T>> Partitions { get; }

    public IEnumerator<Partition<T>> GetEnumerator()
    {
        return Partitions.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerable<Partitioning<T>> Expand()
    {
        (var prefix, var last) = Partitions.ExtractLast();
        return Expand(prefix.ToList(), last);
    }

    private IEnumerable<Partitioning<T>> Expand(List<Partition<T>> prefix, Partition<T> last)
    {
        return last.SplitAscending()
            .Where(tuple => tuple.left.Any() && tuple.right.Any())
            .Select(tuple => new[] { tuple.left, tuple.right })
            .Select(expansion => prefix.Concat(expansion))
            .Select(partitions => new Partitioning<T>(partitions));
    }
}
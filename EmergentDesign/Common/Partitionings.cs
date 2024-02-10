namespace EmergentDesign.Common;

internal static class Partitionings
{
    public static Partitionings<T> Of<T>(IEnumerable<T> toPartitionings)
    {
        return new Partitionings<T>(toPartitionings);
    }
}

internal class Partitionings<T>
{
    public Partitionings(IEnumerable<T> entireSequence)
    {
        EntireSequence = entireSequence.ToList();
    }

    private IEnumerable<T> EntireSequence { get; }

    public IEnumerable<Partitioning<T>> All()
    {
        return new Partitioning<T>(EntireSequence.AsPartition()).ExpandEndlessly(partitioning => partitioning.Expand());
    }
}
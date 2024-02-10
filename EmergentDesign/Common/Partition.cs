using System.Collections;

namespace EmergentDesign.Common;

public class Partition<T>(IEnumerable<T> content) : IEnumerable<T>
{
    public Partition(params T[] content) : this((IEnumerable<T>)content)
    {
    }

    public IEnumerable<T> Content { get; } = content.ToList();

    public IEnumerator<T> GetEnumerator()
    {
        return Content.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public static Partition<T> Empty()
    {
        return new Partition<T>(Enumerable.Empty<T>());
    }

    private Partition<T> Concat(Partition<T> other)
    {
        return new Partition<T>(Content.Concat(other.Content));
    }

    public IEnumerable<(Partition<T> left, Partition<T> right)> Split()
    {
        return Split(Content);
    }

    private IEnumerable<(Partition<T> left, Partition<T> right)> Split(IEnumerable<T> sequence)
    {
        return sequence.IsEmpty()
            ? new[] { (Empty(), Empty()) }
            : Split(sequence.First(), sequence.Skip(1));
    }

    private IEnumerable<(Partition<T> left, Partition<T> right)> Split(T head,
        IEnumerable<T> tail)
    {
        return Combine(TrivialSplit(head), Split(tail).ToList());
    }

    public IEnumerable<(Partition<T> left, Partition<T> right)> SplitAscending()
    {
        return Content.IsEmpty()
            ? new[] { (Empty(), Empty()) }
            : SplitAndPrependLeft(Content.First(), Content.Skip(1));
    }

    private IEnumerable<(Partition<T> left, Partition<T> right)> SplitAndPrependLeft(
        T leftHead, IEnumerable<T> toSplit)
    {
        return Split(toSplit)
            .Select(split => Prepend(leftHead, split.left, split.right));
    }

    private IEnumerable<(Partition<T> left, Partition<T> right)> Combine(
        IEnumerable<(Partition<T> left, Partition<T> right)> head,
        IEnumerable<(Partition<T> left, Partition<T> right)> tail)
    {
        return head.SelectMany(split => tail.Select(
            tuple => (split.left.Concat(tuple.left), split.right.Concat(tuple.right))));
    }

    private (Partition<T> left, Partition<T> right) Prepend(
        T leftHead, Partition<T> left, Partition<T> right)
    {
        return (new Partition<T>(new[] { leftHead }.Concat(left)), right);
    }

    private IEnumerable<(Partition<T> left, Partition<T> right)> TrivialSplit(T item)
    {
        return new (Partition<T> left, Partition<T> right)[]
        {
            (new Partition<T>(item), Empty()),
            (Empty(), new Partition<T>(item))
        };
    }
}
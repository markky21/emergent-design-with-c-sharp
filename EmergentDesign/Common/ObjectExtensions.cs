internal static class ObjectExtensions
{
    public static IEnumerable<T> ExpandEndlessly<T>(this T target, Func<T, IEnumerable<T>> expansion)
    {
        var toExpand = new Queue<T>();
        toExpand.Enqueue(target);

        while (toExpand.TryDequeue(out var current))
        {
            yield return current;
            toExpand.EnqueueMany(expansion(current));
        }
    }
}
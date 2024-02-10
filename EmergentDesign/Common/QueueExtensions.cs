internal static class QueueExtensions
{
    public static void EnqueueMany<T>(this Queue<T> target, IEnumerable<T> objects)
    {
        foreach (var obj in objects)
            target.Enqueue(obj);
    }
}
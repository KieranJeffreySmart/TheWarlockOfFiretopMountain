using System.Collections.Generic;

namespace bookeditor;

public class InMemoryNotificationsQueue
{
    readonly Stack<string> inMemoryQueue = new();

    public bool Any()
    {
        return inMemoryQueue.Count > 0;
    }

    public IEnumerable<char>? Pop()
    {
        return inMemoryQueue.Pop();
    }

    public void Push(string notification)
    {
        inMemoryQueue.Push(notification);
    }
}

using System;
using System.Collections.Generic;

namespace bookeditor;

public class InMemoryNotificationsQueue
{
    Stack<string> inMemoryQueue = new Stack<string>();

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
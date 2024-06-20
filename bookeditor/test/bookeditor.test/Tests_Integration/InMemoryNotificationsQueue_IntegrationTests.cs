namespace bookeditor.test;

public class InMemoryNotificationsQueue_IntegrationTests
{
    [Fact]
    public void PushPopNotification()
    {
        // given I have a notification
        string notiication = "test notification";
        InMemoryNotificationsQueue queue = new InMemoryNotificationsQueue();

        // when I push it to the queue
        queue.Push(notiication);

        // I can pop it off again
        Assert.Equal("test notification", queue.Pop());
    }
}
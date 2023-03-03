using Azure.Messaging.EventGrid;
using Microsoft.Azure.Functions.Worker;

public class TimerTrigger
{
    private readonly EventGridPublisherClient _eventGridPublisherClient;

    public TimerTrigger(EventGridPublisherClient eventGridPublisherClient)
    {
        _eventGridPublisherClient = eventGridPublisherClient;
    }

    [Function(nameof(PocTimerTriggerAsync))]
    public async Task PocTimerTriggerAsync([TimerTrigger("0 0 12 1 * *", RunOnStartup = true)] string bindingParam)
    {
        Console.WriteLine("Sending event");
        var eventGridEvent = new EventGridEvent("testing", "testing", "1.0", "testing");
        var algo = await _eventGridPublisherClient.SendEventAsync(eventGridEvent);
        Console.WriteLine("Event sent");
    }
}
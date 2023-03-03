using Azure.Messaging.EventGrid;
using Microsoft.Azure.Functions.Worker;
using Azure.Messaging.ServiceBus;

public class TimerTrigger
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly EventGridPublisherClient _eventGridPublisherClient;

    public TimerTrigger(ServiceBusClient serviceBusClient, EventGridPublisherClient eventGridPublisherClient)
    {
        _serviceBusClient = serviceBusClient;
        _eventGridPublisherClient = eventGridPublisherClient;
    }

    [Function(nameof(PocTimerTriggerAsync))]
    public async Task PocTimerTriggerAsync([TimerTrigger("0 0 12 1 * *", RunOnStartup = true)] string bindingParam)
    {
        Console.WriteLine("Sending Message");
        await using var sender = _serviceBusClient.CreateSender("sbq-pocif-dev-wus2-1");
        await sender.SendMessageAsync(new ServiceBusMessage("testing"));
        Console.WriteLine("Message sent");

        Console.WriteLine("Sending event");
        var eventGridEvent = new EventGridEvent("testing", "testing", "1.0", "testing");
        var algo = await _eventGridPublisherClient.SendEventAsync(eventGridEvent);
        Console.WriteLine("Event sent");
    }
}
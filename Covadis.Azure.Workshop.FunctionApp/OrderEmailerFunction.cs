using Azure.Messaging.ServiceBus;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Covadis.Azure.Workshop.FunctionApp;

public class OrderEmailerFunction
{
    private readonly ILogger<OrderEmailerFunction> logger;

    public OrderEmailerFunction(ILogger<OrderEmailerFunction> logger)
    {
        this.logger = logger;
    }

    [Function(nameof(OrderEmailerFunction))]
    public async Task Run(
        [ServiceBusTrigger("%ServiceBus:Topics:OrderCreated%", "%ServiceBus:OrderEmailerSubscription%", Connection = "DemoServiceBus")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        logger.LogInformation("New order: {order}", message.Body);
        logger.LogInformation("Sending email...");

        // TODO: Send email to customer

        await messageActions.CompleteMessageAsync(message);
    }
}

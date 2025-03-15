using System.Text.Json;

using Azure.Messaging.ServiceBus;

using Covadis.Azure.Database;
using Covadis.Azure.Database.Models;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Covadis.Azure.Workshop.FunctionApp;

public class OrderPaymentHandlerFunction
{
    private readonly DemoDbContext dbContext;
    private readonly ILogger<OrderPaymentHandlerFunction> logger;

    public OrderPaymentHandlerFunction(
        DemoDbContext dbContext,
        ILogger<OrderPaymentHandlerFunction> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    [Function(nameof(OrderPaymentHandlerFunction))]
    public async Task Run(
        [ServiceBusTrigger("%ServiceBus:Topics:OrderCreated%", "%ServiceBus:OrderPaymentHandlerSubscription%", Connection = "DemoServiceBus")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        logger.LogInformation("New order: {order}", message.Body);
        logger.LogInformation("Handling payment...");

        var order = JsonSerializer.Deserialize<Order>(message.Body.ToString());
        var dbOrder = dbContext.Set<Order>().Find(order?.Id);

        if (dbOrder != null)
        {
            // TODO: Check if payment was successful

            dbOrder.IsPaid = true;

            await dbContext.SaveChangesAsync();
        }

        await messageActions.CompleteMessageAsync(message);
    }
}

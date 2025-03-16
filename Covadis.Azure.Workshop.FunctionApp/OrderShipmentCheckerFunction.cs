using Covadis.Azure.Database;
using Covadis.Azure.Database.Models;

using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Covadis.Azure.Workshop.FunctionApp;

/// <summary>
/// https://en.wikipedia.org/wiki/Cron
/// https://crontab.guru/#*_*_*_*_*
/// https://ncrontab.swimburger.net/
/// </summary>
public class OrderShipmentCheckerFunction
{
    private readonly DemoDbContext dbContext;
    private readonly ILogger logger;

    public OrderShipmentCheckerFunction(
        DemoDbContext dbContext,
        ILogger<OrderShipmentCheckerFunction> logger)
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    [Function("OrderShipmentCheckerFunction")]
    public async Task Run([TimerTrigger("%OrderShipmentCheckerFunction%")] TimerInfo myTimer)
    {
        logger.LogInformation("Checking order shipments...");

        var ordersToBeShipped = await dbContext.Set<Order>()
            .Where(o => o.IsPaid && !o.IsShipped)
            .ToListAsync();

        foreach (var order in ordersToBeShipped)
        {
            logger.LogInformation("Order {orderId} is ready to be shipped", order.Id);
            order.IsShipped = true;
        }

        await dbContext.SaveChangesAsync();
    }
}

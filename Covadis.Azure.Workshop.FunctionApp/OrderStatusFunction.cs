using Covadis.Azure.Database;
using Covadis.Azure.Database.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Covadis.Azure.Workshop.FunctionApp
{
    public class OrderStatusFunction
    {
        private readonly DemoDbContext dbContext;
        private readonly ILogger<OrderStatusFunction> logger;

        public OrderStatusFunction(
            DemoDbContext dbContext,
            ILogger<OrderStatusFunction> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        [Function("OrderStatusFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "orders/{orderId}")] HttpRequest request,
            int orderId)
        {
            logger.LogInformation("Http trigger OrderStatusFunction orders/{orderId} was called", orderId);

            var orderStatus = await dbContext.Set<Order>()
                .Where(x => x.Id == orderId)
                .Select(x => new
                {
                    x.IsPaid,
                    x.IsShipped,
                })
                .FirstOrDefaultAsync();

            return orderStatus == null
                ? new NotFoundResult()
                : new OkObjectResult(orderStatus);
        }
    }
}

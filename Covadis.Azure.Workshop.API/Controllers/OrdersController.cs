using Azure.Messaging.ServiceBus;

using Covadis.Azure.Database;
using Covadis.Azure.Database.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Text;
using System.Text.Json;

namespace Covadis.Azure.Workshop.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly DemoDbContext dbContext;
    private readonly ServiceBusSender serviceBusClient;

    public OrdersController(
        DemoDbContext dbContext, 
        ServiceBusClient serviceBusClient, 
        IConfiguration configuration)
    {
        this.dbContext = dbContext;
        this.serviceBusClient = serviceBusClient.CreateSender(configuration["ServiceBus:Topics:OrderCreated"]);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        var orders = await dbContext.Orders.ToListAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await dbContext.Orders.FindAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(Order order)
    {
        dbContext.Orders.Add(order);

        await dbContext.SaveChangesAsync();
        await serviceBusClient.SendMessageAsync(new ServiceBusMessage(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(order))));

        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, Order order)
    {
        if (id != order.Id)
        {
            return BadRequest();
        }

        dbContext.Entry(order).State = EntityState.Modified;

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!dbContext.Orders.Any(o => o.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var order = await dbContext.Orders.FindAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("All")]
    public async Task<IActionResult> DeleteOrders()
    {
        await dbContext.Set<Order>().ExecuteDeleteAsync();
        return NoContent();
    }
}

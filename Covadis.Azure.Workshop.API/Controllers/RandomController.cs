using Microsoft.AspNetCore.Mvc;

namespace Covadis.Azure.Workshop.API.Controllers;

[ApiController]
[Route("[controller]")]
public class RandomController : ControllerBase
{
    private static readonly Random random = new();

    [HttpGet("Error")]
    public IActionResult GetRandomError()
    {
        if (random.Next(2) == 0)
        {
            throw new Exception("Random error");
        }

        return Ok();
    }
}

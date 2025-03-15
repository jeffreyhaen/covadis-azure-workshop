using Azure.Storage.Blobs;

using Microsoft.AspNetCore.Mvc;

namespace Covadis.Azure.Workshop.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BlobsController(BlobContainerClient blobContainerClient) : ControllerBase
{
    [HttpPost("Upload")]
    public IActionResult Upload(List<IFormFile> files)
    {
        foreach (var file in files)
        {
            var blobClient = blobContainerClient.GetBlobClient(file.FileName);
            blobClient.Upload(file.OpenReadStream(), true);
        }

        return Ok();
    }
}
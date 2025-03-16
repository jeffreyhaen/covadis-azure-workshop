using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Covadis.Azure.Workshop.FunctionApp;

public class BlobTriggerDemoFunction
{
    private readonly ILogger<BlobTriggerDemoFunction> logger;

    public BlobTriggerDemoFunction(ILogger<BlobTriggerDemoFunction> logger)
    {
        this.logger = logger;
    }

    [Function(nameof(BlobTriggerDemoFunction))]
    public async Task Run([BlobTrigger("%Storage:BlobContainer:Demo%/{name}", Connection = "DemoStorageAccount")] Stream stream, string name)
    {
        using var blobStreamReader = new StreamReader(stream);
        var content = await blobStreamReader.ReadToEndAsync();

        logger.LogInformation("C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}", name, content);
    }
}

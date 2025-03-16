using Azure.Messaging.ServiceBus;
using Azure.Storage.Blobs;

using Covadis.Azure.Database;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var services = builder.Services;
var configuration = builder.Configuration;

services.AddApplicationInsightsTelemetry();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<DemoDbContext>(optionsBuilder =>
{
    optionsBuilder.UseSqlServer(configuration.GetConnectionString("DemoDatabase"));

    if (configuration["Environment"] == "Local")
    {
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }
},
contextLifetime: ServiceLifetime.Scoped);

services.AddScoped(_ => new ServiceBusClient(configuration.GetConnectionString("DemoServiceBus")));
services.AddScoped(x => new BlobServiceClient(configuration.GetConnectionString("DemoStorageAccount")).GetBlobContainerClient(configuration["Storage:BlobContainer:Demo"]));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.EnablePersistAuthorization();
    options.EnableTryItOutByDefault();
});

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

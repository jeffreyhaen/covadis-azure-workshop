using Covadis.Azure.Database;

using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((builder, services) =>
    {
        var configuration = builder.Configuration;
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddDbContext<DemoDbContext>(optionsBuilder =>
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DemoDatabase"));

            if (configuration["Environment"] == "Local")
            {
                optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            }
        },
        contextLifetime: ServiceLifetime.Scoped);
    })
    .ConfigureLogging((hostingContext, loggingBuilder) =>
    {
        var loggingConfig = hostingContext.Configuration.GetSection("Logging");
        loggingBuilder.AddConfiguration(loggingConfig);
        loggingBuilder.AddConsole();
        loggingBuilder.AddApplicationInsights();
    })
    .Build();

host.Run();

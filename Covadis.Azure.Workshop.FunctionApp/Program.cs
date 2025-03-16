using Covadis.Azure.Database;

using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = new HostBuilder()
    .ConfigureAppConfiguration((context, builder) =>
    {
        builder
            .AddJsonFile(Path.Combine(context.HostingEnvironment.ContentRootPath, "appsettings.json"), optional: true, reloadOnChange: false)
            .AddJsonFile(Path.Combine(context.HostingEnvironment.ContentRootPath, $"appsettings.{context.HostingEnvironment.EnvironmentName}.json"), optional: true, reloadOnChange: false)
            .AddEnvironmentVariables();
    })
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((builder, services) =>
    {
        var configuration = builder.Configuration;

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
    })
    .Build();

host.Run();

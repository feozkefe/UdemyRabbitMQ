using FileCreatorWorkerService;
using FileCreatorWorkerService.Models;
using FileCreatorWorkerService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        services.AddHostedService<Worker>();

        // Retrieve RabbitMQ URI from appsettings
        services.AddSingleton(sp =>
        {
            var rabbitMqUri = new Uri(configuration.GetConnectionString("RabbitMQ"));
            return new ConnectionFactory() { Uri = rabbitMqUri, DispatchConsumersAsync = true };
        });

        services.AddSingleton<RabbitMQClientService>();

        services.AddDbContext<AdventureWorks2019Context>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        });
    })
    .Build();

await host.RunAsync();
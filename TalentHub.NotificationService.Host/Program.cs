using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TalentHub.NotificationService.Application.Models;
using TalentHub.NotificationService.Host.Consumers;
using TalentHub.NotificationService.Host.Settings;
using TalentHub.NotificationService.Infrastructure.Data;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddOptions<ApplicationConfiguration>()
    .BindConfiguration(nameof(ApplicationConfiguration));
builder.Services.AddOptions<SmtpConfiguration>()
    .BindConfiguration(nameof(SmtpConfiguration));
builder.Services.AddOptions<FirebaseConfiguration>()
    .BindConfiguration(nameof(FirebaseConfiguration));
builder.Services.AddOptions<RabbitMqConfiguration>()
    .BindConfiguration(nameof(RabbitMqConfiguration));

builder.Services.AddDbContext<NotificationDbContext>((sp, options) =>
{
    var settings = sp.GetRequiredService<IOptions<ApplicationConfiguration>>();
    options.EnableSensitiveDataLogging();
    options.UseNpgsql(settings.Value.ConnectionString);
});

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<NotificationConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        var configuration = context.GetService<IOptions<RabbitMqConfiguration>>()
                       ?? throw new ConfigurationException($"Lack of '{nameof(RabbitMqConfiguration)}' settings");

        var rabbitMqConfiguration = configuration.Value;

        cfg.Host(rabbitMqConfiguration.Host, rmqCfg =>
        {
            rmqCfg.Username(rabbitMqConfiguration.Username);
            rmqCfg.Password(rabbitMqConfiguration.Password);
        });
    });
});

var host = builder.Build();
host.Run();

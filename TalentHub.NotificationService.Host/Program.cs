using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TalentHub.NotificationService.Application.Models;
using TalentHub.NotificationService.Host.Configurations;
using TalentHub.NotificationService.Host.Consumers;
using TalentHub.NotificationService.Host.Settings;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json");

builder.Services.AddOptions<SeqConfiguration>()
    .BindConfiguration(nameof(SeqConfiguration));
builder.Services.AddOptions<SmtpConfiguration>()
    .BindConfiguration(nameof(SmtpConfiguration));
builder.Services.AddOptions<FirebaseConfiguration>()
    .BindConfiguration(nameof(FirebaseConfiguration));
builder.Services.AddOptions<RabbitMqConfiguration>()
    .BindConfiguration(nameof(RabbitMqConfiguration));

var seqOptions = builder.Services.BuildServiceProvider()
    .GetService<IOptions<SeqConfiguration>>();

if (seqOptions == null)
{
    throw new InvalidOperationException($"Could not find '{nameof(SeqConfiguration)}'");
}

builder.Logging.ClearProviders();
builder.Logging.AddSeq(seqOptions.Value.ServerUrl, seqOptions.Value.ApiKey);

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

using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Application.Models;
using TalentHub.NotificationService.Application.Providers;
using TalentHub.NotificationService.Application.Services.Clients;
using TalentHub.NotificationService.Host;
using TalentHub.NotificationService.Host.Configurations;
using TalentHub.NotificationService.Host.Consumers;
using TalentHub.NotificationService.Host.Extensions;
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

builder.Services.AddScoped<INotificationSenderFactory, NotificationSenderFactory>();
builder.Services.AddScoped<INotificationSenderProvider, NotificationSenderProvider>();
builder.Services.AddScoped<IHostedService, MassTransitService>();
builder.Services.AddScoped<IConsumer, NotificationConsumer>();
builder.Services.AddScoped<IHostedService, MassTransitService>();

builder.Services
    .AddHttpClient<IUserServiceClient, UserServiceClient>((context, client) =>
    {
        var configuration = context.GetService<IOptions<UserServiceClientConfiguration>>()
                            ?? throw new ConfigurationException($"Lack of '{nameof(UserServiceClientConfiguration)}' settings");

        var userServiceClientConfiguration = configuration.Value;
        
        client.BaseAddress = new Uri(userServiceClientConfiguration.Endpoint);
    })
    .AddPolicyHandler((context, _) =>
    {
        var configuration = context.GetService<IOptions<UserServiceClientErrorPolicy>>()
                            ?? throw new ConfigurationException($"Lack of '{nameof(UserServiceClientErrorPolicy)}' settings");
        
        var retryConfiguration = configuration.Value;
        
        return Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(x => !x.IsSuccessStatusCode)
            .WaitAndRetryAsync(
                retryConfiguration.RetryCount,
                _ => TimeSpan.FromSeconds(retryConfiguration.SleepDuration)
            );
    });


builder.Services.RegisterMapper();

var seqOptions = builder.Services.BuildServiceProvider()
    .GetService<IOptions<SeqConfiguration>>();

if (seqOptions == null)
{
    throw new InvalidOperationException($"Could not find '{nameof(SeqConfiguration)}'");
}

builder.Logging.ClearProviders();
builder.Logging.AddSeq(seqOptions.Value.ServerUrl);

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
        
        cfg.ReceiveEndpoint(rabbitMqConfiguration.QueueName, e =>
        {
            e.ConfigureConsumer<NotificationConsumer>(context);
        });
    });
});

var host = builder.Build();
host.Run();

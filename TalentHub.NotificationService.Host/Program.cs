using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Application.Models;
using TalentHub.NotificationService.Application.Providers;
using TalentHub.NotificationService.Host;
using TalentHub.NotificationService.Host.Configurations;
using TalentHub.NotificationService.Host.Consumers;
using TalentHub.NotificationService.Host.Extensions;
using TalentHub.NotificationService.Host.Settings;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddOptions<SeqConfiguration>()
    .BindConfiguration(nameof(SeqConfiguration));
builder.Services.AddOptions<SmtpConfiguration>()
    .BindConfiguration(nameof(SmtpConfiguration));
builder.Services.AddOptions<FirebaseConfiguration>()
    .BindConfiguration(nameof(FirebaseConfiguration));
builder.Services.AddOptions<RabbitMqConfiguration>()
    .BindConfiguration(nameof(RabbitMqConfiguration));
builder.Services.AddOptions<UserServiceClientConfiguration>()
    .BindConfiguration(nameof(UserServiceClientConfiguration));
builder.Services.AddOptions<HttpClientErrorPolicy>()
    .BindConfiguration(nameof(HttpClientErrorPolicy));

builder.Services.RegisterHttpClients();
builder.Services.RegisterMapper();

builder.Services.AddScoped<INotificationSenderFactory, NotificationSenderFactory>();
builder.Services.AddScoped<INotificationSenderProvider, NotificationSenderProvider>();
builder.Services.AddScoped<IConsumer, NotificationConsumer>();

builder.Services.RegisterMassTransitConsumer();
builder.Services.AddHostedService<MassTransitService>();

builder.Logging.RegisterSeq();

var host = builder.Build();
host.Run();
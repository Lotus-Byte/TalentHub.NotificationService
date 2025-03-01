using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Application.Providers;
using TalentHub.NotificationService.Host;
using TalentHub.NotificationService.Host.Consumers;
using TalentHub.NotificationService.Host.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables();

builder.Services.RegisterOptions();
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
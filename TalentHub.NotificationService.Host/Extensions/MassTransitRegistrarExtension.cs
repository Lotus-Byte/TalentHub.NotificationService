using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using TalentHub.NotificationService.Host.Consumers;
using TalentHub.NotificationService.Host.Settings;

namespace TalentHub.NotificationService.Host.Extensions;

public static class MassTransitRegistrarExtension
{
    public static IServiceCollection RegisterMassTransitConsumer(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<NotificationConsumer>();
            x.UsingRabbitMq((context, cfg) =>
            {
                var configuration = context.GetService<IOptions<RabbitMqConfiguration>>()
                                    ?? throw new ConfigurationException($"Lack of '{nameof(RabbitMqConfiguration)}' settings");

                var rabbitMqConfiguration = configuration.Value;
         
                cfg.Host(rabbitMqConfiguration.Host, rabbitMqConfiguration.VirtualHost, h =>
                {
                    h.Username(rabbitMqConfiguration.Username);
                    h.Password(rabbitMqConfiguration.Password);
                });

                cfg.ReceiveEndpoint(rabbitMqConfiguration.QueueName, e =>
                {
                    e.ConfigureConsumer<NotificationConsumer>(context);
            
                    e.Bind("notification_event", bc =>
                    {
                        bc.ExchangeType = ExchangeType.Direct;
                        bc.RoutingKey = rabbitMqConfiguration.QueueName;
                    });
                });
            });
        });
        
        return services;
    }
}
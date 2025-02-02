using MassTransit;
using Microsoft.Extensions.Logging;
using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Host.Models;

namespace TalentHub.NotificationService.Host.Consumers;

public class NotificationConsumer : IConsumer<NotificationMessageModel>
{
    private readonly INotificationSenderProvider _provider;
    private readonly ILogger<NotificationConsumer> _logger;

    public NotificationConsumer(
        INotificationSenderProvider provider,
        ILogger<NotificationConsumer> logger)
    {
        _provider = provider;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<NotificationMessageModel> context)
    {
        throw new NotImplementedException();
    }
}
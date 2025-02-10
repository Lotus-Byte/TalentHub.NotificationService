using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Application.DTO;
using TalentHub.NotificationService.Host.Models;

namespace TalentHub.NotificationService.Host.Consumers;

public class NotificationConsumer : IConsumer<NotificationMessageModel>
{
    private readonly INotificationSenderProvider _provider;
    private readonly IMapper _mapper;
    private readonly ILogger<NotificationConsumer> _logger;

    public NotificationConsumer(
        INotificationSenderProvider provider,
        IMapper mapper,
        ILogger<NotificationConsumer> logger)
    {
        _provider = provider;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<NotificationMessageModel> context)
    {
        var message = context.Message;
        
        _logger.LogInformation("Received notification message for UserId: {UserId}", message.UserId);

        try
        {
            var senders = _provider.Provide(_mapper.Map<UserNotificationSettingsDto>(message.UserNotificationSettings));

            foreach (var sender in senders)
            {
                await sender.SendAsync(_mapper.Map<NotificationDto>(message.Notification));
                _logger.LogInformation("Sent notification message for UserId: {UserId}", message.UserId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending notification message for UserId: {UserId}", message.UserId);
            _logger.LogError(ex, "Traceback for UserId: {StackTrace}", ex.StackTrace);
        }
    }
}
using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Application.DTO;
using TalentHub.NotificationService.Host.Models;

namespace TalentHub.NotificationService.Host.Consumers;

public class NotificationConsumer : IConsumer<NotificationEventModel>
{
    private readonly IUserServiceClient _userServiceClient;
    private readonly INotificationSenderProvider _provider;
    private readonly IMapper _mapper;
    private readonly ILogger<NotificationConsumer> _logger;

    public NotificationConsumer(
        IUserServiceClient userServiceClient,
        INotificationSenderProvider provider,
        IMapper mapper,
        ILogger<NotificationConsumer> logger)
    {
        _userServiceClient = userServiceClient;
        _provider = provider;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<NotificationEventModel> context)
    {
        var message = context.Message;
        
        _logger.LogInformation("Received notification message for UserId: {UserId}", message.UserId);

        try
        {
            var settings = await _userServiceClient.GetUserNotificationSettingsAsync(message.UserId.ToString());
            var senders = _provider.Provide(settings);

            foreach (var sender in senders)
            {
                await sender.SendAsync(_mapper.Map<NotificationDto>(message.Notification));
                _logger.LogInformation("Sent notification message for UserId: {UserId}. [{Title}] : {Content}", 
                    message.UserId, message.Notification.Title, message.Notification.Content);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending notification message for UserId: {UserId}", message.UserId);
            _logger.LogError(ex, "Traceback for UserId: {StackTrace}", ex.StackTrace);
        }
    }
}
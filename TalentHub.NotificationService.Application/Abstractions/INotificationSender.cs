using TalentHub.NotificationService.Application.DTO;

namespace TalentHub.NotificationService.Application.Abstractions;

public interface INotificationSender
{
    Task SendAsync(NotificationDto notification);
}
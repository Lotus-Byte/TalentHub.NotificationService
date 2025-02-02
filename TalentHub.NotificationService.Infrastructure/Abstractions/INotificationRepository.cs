using TalentHub.NotificationService.Infrastructure.Models;

namespace TalentHub.NotificationService.Infrastructure.Abstractions;

public interface INotificationRepository
{
    Task<NotificationMessage?> AddNotificationAsync(NotificationMessage? notificationMessage);
}
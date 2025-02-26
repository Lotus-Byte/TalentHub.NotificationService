using TalentHub.NotificationService.Application.DTO;

namespace TalentHub.NotificationService.Application.Abstractions;

public interface IFirebaseServiceClient
{
    Task PostFirebaseNotificationAsync(NotificationDto notification, string deviceToken);
}
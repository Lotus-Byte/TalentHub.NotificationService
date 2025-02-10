using TalentHub.NotificationService.Application.DTO;

namespace TalentHub.NotificationService.Application.Abstractions;

public interface INotificationSenderProvider
{
    IEnumerable<INotificationSender> Provide(UserNotificationSettingsDto notificationSettings);
}
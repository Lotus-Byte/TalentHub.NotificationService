using TalentHub.NotificationService.Application.Models;

namespace TalentHub.NotificationService.Application.Abstractions;

public interface INotificationSenderProvider
{
    IEnumerable<INotificationSender> Provide(UserNotificationSettings notificationSettings);
}
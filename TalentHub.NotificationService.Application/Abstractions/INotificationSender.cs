namespace TalentHub.NotificationService.Application.Abstractions;

public interface INotificationSender
{
    Task SendAsync(INotification notification);
}
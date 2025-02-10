namespace TalentHub.NotificationService.Host.Models;

public class NotificationMessageModel
{
    public Guid UserId { get; init; }
    public NotificationModel Notification { get; init; }
    public UserNotificationSettingsModel UserNotificationSettings { get; init; }
    public DateTime Ts { get; init; }
}
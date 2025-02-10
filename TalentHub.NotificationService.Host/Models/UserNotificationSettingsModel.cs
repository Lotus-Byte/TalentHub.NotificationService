namespace TalentHub.NotificationService.Host.Models;

public class UserNotificationSettingsModel
{
    public EmailNotificationSettingsModel Email { get; set; }
    public PushNotificationSettingsModel Push { get; set; }
}
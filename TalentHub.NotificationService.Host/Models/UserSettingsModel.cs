namespace TalentHub.NotificationService.Host.Models;

public class UserSettingsModel
{
    public EmailNotificationSettingsModel Email { get; set; }
    public PushNotificationSettingsModel Push { get; set; }
}
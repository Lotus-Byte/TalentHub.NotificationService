namespace TalentHub.NotificationService.Host.Models;

public class PushNotificationSettingsModel
{
    public bool Enabled { get; set; }
    public string DeviceToken { get; set; }
}
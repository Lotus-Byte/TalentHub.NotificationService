namespace TalentHub.NotificationService.Application.Models;

public class PushNotificationSettings
{
    public bool Enabled { get; set; }
    public string DeviceToken { get; set; }
}
namespace TalentHub.NotificationService.Application.DTO;

public class PushNotificationSettingsDto
{
    public bool Enabled { get; set; }
    public string DeviceToken { get; set; }
}
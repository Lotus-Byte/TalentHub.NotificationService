namespace TalentHub.NotificationService.Application.DTO;

public class UserNotificationSettingsDto
{
    public PushNotificationSettingsDto Push { get; set; }
    public EmailNotificationSettingsDto Email { get; set; }
}
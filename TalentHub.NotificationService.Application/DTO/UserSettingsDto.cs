namespace TalentHub.NotificationService.Application.DTO;

public class UserSettingsDto
{
    public PushNotificationSettingsDto Push { get; set; }
    public EmailNotificationSettingsDto Email { get; set; }
}
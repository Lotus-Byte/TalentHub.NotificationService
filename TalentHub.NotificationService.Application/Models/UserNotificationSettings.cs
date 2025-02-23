namespace TalentHub.NotificationService.Application.Models;

public class UserNotificationSettings
{
    public PushNotificationSettings Push { get; set; }
    public EmailNotificationSettings Email { get; set; }
}
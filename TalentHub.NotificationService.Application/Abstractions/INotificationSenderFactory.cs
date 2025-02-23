using TalentHub.NotificationService.Application.Models;

namespace TalentHub.NotificationService.Application.Abstractions;

public interface INotificationSenderFactory
{
    INotificationSender CreateEmailSender(SmtpConfiguration config, EmailNotificationSettings settings);
    INotificationSender CreatePushSender(FirebaseConfiguration config, PushNotificationSettings settings);
}
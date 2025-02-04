using TalentHub.NotificationService.Application.DTO;
using TalentHub.NotificationService.Application.Models;

namespace TalentHub.NotificationService.Application.Abstractions;

public interface INotificationSenderFactory
{
    INotificationSender CreateEmailSender(SmtpConfiguration config, EmailNotificationSettingsDto settings);
    INotificationSender CreatePushSender(FirebaseConfiguration config, PushNotificationSettingsDto settings);
}
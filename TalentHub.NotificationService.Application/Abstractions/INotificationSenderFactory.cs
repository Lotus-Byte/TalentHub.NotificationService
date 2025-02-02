using TalentHub.NotificationService.Application.Models;

namespace TalentHub.NotificationService.Application.Abstractions;

public interface INotificationSenderFactory
{
    INotificationSender CreateEmailSender(SmtpConfiguration config);
    INotificationSender CreatePushSender(FirebaseConfiguration config);
}
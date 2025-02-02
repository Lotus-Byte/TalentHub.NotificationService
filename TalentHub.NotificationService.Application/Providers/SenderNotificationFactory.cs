using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Application.Models;
using TalentHub.NotificationService.Application.Services;

namespace TalentHub.NotificationService.Application.Providers;

public class SenderNotificationFactory : INotificationSenderFactory
{
    public INotificationSender CreateEmailSender(SmtpConfiguration config)
    {
        ArgumentNullException.ThrowIfNull(config);

        return new EmailNotificationSender(config);
    }

    public INotificationSender CreatePushSender(FirebaseConfiguration config)
    {
        ArgumentNullException.ThrowIfNull(config);

        return new MobileAppNotificationSender(config);
    }
}
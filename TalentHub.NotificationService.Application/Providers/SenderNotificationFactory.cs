using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Application.DTO;
using TalentHub.NotificationService.Application.Models;
using TalentHub.NotificationService.Application.Services;

namespace TalentHub.NotificationService.Application.Providers;

public class SenderNotificationFactory : INotificationSenderFactory
{
    public INotificationSender CreateEmailSender(SmtpConfiguration config, EmailNotificationSettingsDto settings)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(settings);

        return new EmailNotificationSender(config, settings);
    }

    public INotificationSender CreatePushSender(FirebaseConfiguration config, PushNotificationSettingsDto settings)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(settings);

        return new MobileAppNotificationSender(config, settings);
    }
}
using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Application.Models;
using TalentHub.NotificationService.Application.Services;

namespace TalentHub.NotificationService.Application.Providers;

public class NotificationSenderFactory : INotificationSenderFactory
{
    private readonly IFirebaseServiceClient _firebaseClient;
    public NotificationSenderFactory(IFirebaseServiceClient firebaseClient) => _firebaseClient = firebaseClient;
    public INotificationSender CreateEmailSender(SmtpConfiguration config, EmailNotificationSettings settings)
    {
        ArgumentNullException.ThrowIfNull(config);
        ArgumentNullException.ThrowIfNull(settings);

        return new EmailNotificationSender(config, settings);
    }

    public INotificationSender CreatePushSender(PushNotificationSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        return new MobileAppNotificationSender(_firebaseClient, settings);
    }
}
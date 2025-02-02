using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Application.DTO;
using TalentHub.NotificationService.Application.Models;

namespace TalentHub.NotificationService.Application.Providers;

public class NotificationSenderProvider : INotificationSenderProvider
{
    private readonly INotificationSenderFactory _senderFactory;
    private readonly SmtpConfiguration _smtpConfiguration;
    private readonly FirebaseConfiguration _firebaseConfiguration;
    private readonly ILogger<NotificationSenderProvider> _logger;
    
    public NotificationSenderProvider(
        INotificationSenderFactory senderFactory,
        IOptions<SmtpConfiguration> smtpConfiguration, 
        IOptions<FirebaseConfiguration> firebaseConfiguration, 
        ILogger<NotificationSenderProvider> logger)
    {
        _senderFactory = senderFactory;
        _smtpConfiguration = smtpConfiguration.Value ?? throw new ArgumentNullException(nameof(smtpConfiguration));
        _firebaseConfiguration = firebaseConfiguration.Value ?? throw new ArgumentNullException(nameof(firebaseConfiguration));
        _logger = logger;
    }

    public IEnumerable<INotificationSender> Provide(UserSettingsDto settings)
    {
        ArgumentNullException.ThrowIfNull(settings);
        
        var senders = new List<INotificationSender>();
        
        if (settings.EmailNotificationEnabled)
        {
            _logger.LogInformation("Email notification sender selected.");
            senders.Add(_senderFactory.CreateEmailSender(_smtpConfiguration));
        }
        
        if (settings.PushNotificationEnabled)
        {
            _logger.LogInformation("Push notification sender selected.");
            senders.Add(_senderFactory.CreatePushSender(_firebaseConfiguration));
        }

        return senders;
    }
}
using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Application.Models;

namespace TalentHub.NotificationService.Application.Services;

public class EmailNotificationSender : INotificationSender
{
    public EmailNotificationSender(SmtpConfiguration config)
    {
        throw new NotImplementedException();
    }

    public async Task SendAsync(INotification notification)
    {
        throw new NotImplementedException();
    }
}
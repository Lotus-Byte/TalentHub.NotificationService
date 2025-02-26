using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Application.DTO;
using TalentHub.NotificationService.Application.Models;

namespace TalentHub.NotificationService.Application.Services;

public class EmailNotificationSender : INotificationSender
{
    private const string FromAddress = "noreply@talenthub.com";
    private const string Subject = "TalentHub service notification";
    
    private readonly SmtpConfiguration _smtpConfiguration;
    private readonly EmailNotificationSettings _notificationSettings;

    public EmailNotificationSender(SmtpConfiguration smtpConfiguration, EmailNotificationSettings notificationSettings)
    {
        _smtpConfiguration = smtpConfiguration;
        _notificationSettings = notificationSettings;
    }

    public async Task SendAsync(NotificationDto notification)
    {
        // TODO: next code is an example of the service implementation
        // using var client = new SmtpClient(_smtpConfiguration.Server, _smtpConfiguration.Port);
        // client.Credentials = new NetworkCredential(_smtpConfiguration.Username, _smtpConfiguration.Password);
        // client.EnableSsl = _smtpConfiguration.EnableSsl;
        //
        //
        // var mailMessage = new MailMessage(FromAddress, _notificationSettings.Address, $"{Subject}:{notification.Title}", notification.Content);
        //
        // try
        // {
        //     await client.SendMailAsync(mailMessage);
        // }
        // catch (Exception ex)
        // {
        //     throw new Exception($"Failed to send notification: {ex.Message}");
        // }
    }
}
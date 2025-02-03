using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Application.DTO;
using TalentHub.NotificationService.Application.Models;

namespace TalentHub.NotificationService.Application.Services;

public class EmailNotificationSender : INotificationSender
{
    private const string FromAddress = "noreply@talenthub.com";
    private const string Subject = "TalentHub service notification";
    
    private readonly SmtpConfiguration _smtpConfiguration;

    public EmailNotificationSender(SmtpConfiguration smtpConfiguration) =>
        _smtpConfiguration = smtpConfiguration;

    public async Task SendAsync(NotificationDto notification)
    {
        using var client = new SmtpClient(_smtpConfiguration.Server, _smtpConfiguration.Port);
        client.Credentials = new NetworkCredential(_smtpConfiguration.Username, _smtpConfiguration.Password);
        client.EnableSsl = _smtpConfiguration.EnableSsl;

        // TODO: GET AN EMAIL FROM  <WHERE> ???
        var mailMessage = new MailMessage(FromAddress, "", $"{Subject}:{notification.Title}", notification.Content);

        await client.SendMailAsync(mailMessage);
    }
}
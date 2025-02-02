namespace TalentHub.NotificationService.Application.Models;

public class SmtpConfiguration
{
    public string Server { get; set; }
    public string Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
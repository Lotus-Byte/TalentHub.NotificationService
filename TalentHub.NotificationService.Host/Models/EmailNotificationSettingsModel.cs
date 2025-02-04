namespace TalentHub.NotificationService.Host.Models;

public class EmailNotificationSettingsModel
{
    public bool Enabled { get; set; }
    public string Address { get; set; }
}
namespace TalentHub.NotificationService.Host.Models;

public class NotificationMessageModel
{
    public Guid UserId { get; init; }
    public string Text { get; init; }
    public UserSettingsModel SettingsModel { get; init; }
    public DateTime Ts { get; init; }
}
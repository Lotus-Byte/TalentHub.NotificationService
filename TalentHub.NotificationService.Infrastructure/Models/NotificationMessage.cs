namespace TalentHub.NotificationService.Infrastructure.Models;

public class NotificationMessage
{
    public Guid UserId { get; init; }
    public string Text { get; init; }
    public bool EmailEnabled { get; init; }
    public bool MobileAppPushEnabled { get; init; }
    public DateTime Ts { get; init; }
}
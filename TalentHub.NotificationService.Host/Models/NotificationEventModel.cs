namespace TalentHub.NotificationService.Host.Models;

public class NotificationEventModel
{
    public Guid UserId { get; init; }
    public NotificationModel Notification { get; init; }
    public DateTime Ts { get; init; }
}
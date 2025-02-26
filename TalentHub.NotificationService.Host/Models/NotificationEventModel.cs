namespace TalentHub.NotificationService.Host.Models;

public class NotificationEventModel
{
    public Guid UserId { get; init; }
    public NotificationModel Notification { get; init; }
    public DateTimeOffset Ts { get; init; }
}
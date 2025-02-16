namespace TalentHub.NotificationService.Application.DTO;

public class NotificationEventDto
{
    public Guid UserId { get; init; }
    public NotificationDto Notification { get; init; }
    public UserNotificationSettingsDto UserNotificationSettings { get; init; }
    public DateTime Ts { get; init; }
}
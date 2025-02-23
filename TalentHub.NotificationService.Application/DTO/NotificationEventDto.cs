using TalentHub.NotificationService.Application.Models;

namespace TalentHub.NotificationService.Application.DTO;

public class NotificationEventDto
{
    public Guid UserId { get; init; }
    public NotificationDto Notification { get; init; }
    public UserNotificationSettings UserNotificationSettings { get; init; }
    public DateTime Ts { get; init; }
}
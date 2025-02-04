namespace TalentHub.NotificationService.Application.DTO;

public class NotificationMessageDto
{
    public Guid UserId { get; init; }
    public NotificationDto Notification { get; init; }
    public UserSettingsDto UserSettings { get; init; }
    public DateTime Ts { get; init; }
}
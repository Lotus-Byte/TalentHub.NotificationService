namespace TalentHub.NotificationService.Application.DTO;

public class NotificationMessageDto
{
    public Guid UserId { get; init; }
    public string Text { get; init; }
    public UserSettingsDto UserSettings { get; init; }
    public DateTime Created { get; init; }
}
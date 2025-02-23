using TalentHub.NotificationService.Application.Models;

namespace TalentHub.NotificationService.Application.Abstractions;

public interface IUserServiceClient
{
    Task<UserNotificationSettings?> GetUserNotificationSettingsAsync(string userId);
}
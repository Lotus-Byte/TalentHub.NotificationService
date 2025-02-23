namespace TalentHub.NotificationService.Application.Models;

public class UserServiceClientErrorPolicy
{
    public int RetryCount { get; set; }
    public int SleepDuration { get; set; }
}
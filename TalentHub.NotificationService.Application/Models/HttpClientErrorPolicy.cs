namespace TalentHub.NotificationService.Application.Models;

public class HttpClientErrorPolicy
{
    public int RetryCount { get; set; }
    public int SleepDuration { get; set; }
}
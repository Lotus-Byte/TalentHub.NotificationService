namespace TalentHub.NotificationService.Host.Settings;

public class RabbitMqConfiguration
{
    public string Host { get; init; }
    public string VirtualHost { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
    public string QueueName { get; init; }
}
using System.Text;
using System.Text.Json;
using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Application.DTO;
using TalentHub.NotificationService.Application.Models;

namespace TalentHub.NotificationService.Application.Services;

public class MobileAppNotificationSender : INotificationSender
{
    private readonly FirebaseConfiguration _firebaseConfiguration;
    private readonly PushNotificationSettingsDto _notificationSettings;
    public MobileAppNotificationSender(
        FirebaseConfiguration firebaseConfiguration, 
        PushNotificationSettingsDto notificationSettings)
    {
        _firebaseConfiguration = firebaseConfiguration;
        _notificationSettings = notificationSettings;
    }

    public async Task SendAsync(NotificationDto notification)
    {
        using var httpClient = new HttpClient();
        
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={_firebaseConfiguration.ServerKey}");
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={_firebaseConfiguration.SenderId}");

        var message = new
        {
            to = _notificationSettings.DeviceToken,
            notification = new
            {
                title = notification.Title,
                body = notification.Content
            }
        };

        var json = JsonSerializer.Serialize(message);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(_firebaseConfiguration.Url, content);
        var responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to send notification, " +
                                           $"Status Code: {response.StatusCode}, Message: {responseString}");
        }
    }
}
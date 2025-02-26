using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Application.DTO;
using TalentHub.NotificationService.Application.Models;

namespace TalentHub.NotificationService.Application.Services.Clients;

public class FirebaseServiceClient : IFirebaseServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly FirebaseConfiguration _configuration;
    private readonly ILogger<FirebaseServiceClient> _logger;
    
    public FirebaseServiceClient(
        HttpClient httpClient, 
        IOptions<FirebaseConfiguration> configuration,
        ILogger<FirebaseServiceClient> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration.Value;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task PostFirebaseNotificationAsync(NotificationDto notification, string deviceToken)
    {
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", $"key={_configuration.ServerKey}");
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Sender", $"id={_configuration.SenderId}");

        var message = new
        {
            to = deviceToken,
            notification = new
            {
                title = notification.Title,
                body = notification.Content
            }
        };

        var json = JsonSerializer.Serialize(message);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(_configuration.Url, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Firebase notification failed with status code {StatusCode}", response.StatusCode);
            
                throw new HttpRequestException($"Failed to send notification, " +
                                               $"Status Code: {response.StatusCode}, Message: {responseString}");
            }
        
            _logger.LogWarning("Firebase notification sent for device token '{DeviceToken}'.", deviceToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending firebase notification for device token: {DeviceToken}", deviceToken);
            throw;
        }
    }
}
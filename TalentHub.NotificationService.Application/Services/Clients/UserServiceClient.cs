using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Application.Models;

namespace TalentHub.NotificationService.Application.Services.Clients;

public class UserServiceClient : IUserServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<UserServiceClient> _logger;
    
    public UserServiceClient(
        HttpClient httpClient, 
        ILogger<UserServiceClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<UserNotificationSettings?> GetUserNotificationSettingsAsync(string userId)
    {
        _logger.LogInformation("Fetching user settings for user ID: {UserId}", userId);

        if (!Guid.TryParse(userId, out _))
        {
            _logger.LogWarning("Invalid user ID format: {UserId}", userId);
            throw new ArgumentException("Invalid user ID format. Expected a GUID.", nameof(userId));
        }

        try
        {
            var response = await _httpClient.GetAsync(userId);

            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    _logger.LogWarning("User settings not found for user ID: {UserId}", userId);
                    throw new KeyNotFoundException($"User settings for user '{userId}' not found.");
                case HttpStatusCode.BadRequest:
                {
                    var errorDetails = await response.Content.ReadAsStringAsync();
                    _logger.LogWarning("Bad request for user ID {UserId}: {ErrorDetails}", userId, errorDetails);
                    throw new ArgumentException($"Invalid request: {errorDetails}");
                }
                default:
                    response.EnsureSuccessStatusCode();

                    return await response.Content.ReadFromJsonAsync<UserNotificationSettings>();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching user settings for user ID: {UserId}", userId);
            throw;
        }
    }
}
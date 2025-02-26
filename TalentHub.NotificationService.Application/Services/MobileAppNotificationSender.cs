using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Application.DTO;
using TalentHub.NotificationService.Application.Models;

namespace TalentHub.NotificationService.Application.Services;

public class MobileAppNotificationSender : INotificationSender
{
    private readonly IFirebaseServiceClient _firebaseClient;
    private readonly PushNotificationSettings _notificationSettings;
    
    public MobileAppNotificationSender(IFirebaseServiceClient firebaseClient, PushNotificationSettings notificationSettings)
    {
        _firebaseClient = firebaseClient;
        _notificationSettings = notificationSettings;  
    } 

    public async Task SendAsync(NotificationDto notification)
    {
        // TODO: next code is an example of the service implementation
        // await  _firebaseClient.PostFirebaseNotificationAsync(notification, _notificationSettings.DeviceToken);
    }
}
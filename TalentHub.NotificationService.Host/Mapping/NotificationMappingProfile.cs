using AutoMapper;
using TalentHub.NotificationService.Application.DTO;
using TalentHub.NotificationService.Host.Models;

namespace TalentHub.NotificationService.Host.Mapping;

public class NotificationMappingProfile : Profile
{
    public NotificationMappingProfile()
    {
        CreateMap<UserSettingsDto, UserSettingsModel>();
        CreateMap<NotificationDto, NotificationModel>();
        CreateMap<EmailNotificationSettingsDto, EmailNotificationSettingsModel>();
        CreateMap<PushNotificationSettingsDto, PushNotificationSettingsModel>();
    }
}
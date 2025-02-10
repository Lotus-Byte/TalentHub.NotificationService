using AutoMapper;
using TalentHub.NotificationService.Application.DTO;
using TalentHub.NotificationService.Host.Models;

namespace TalentHub.NotificationService.Host.Mapping;

public class NotificationMappingProfile : Profile
{
    public NotificationMappingProfile()
    {
        CreateMap<NotificationMessageModel, NotificationMessageDto>()
            .ForMember(dest => dest.Notification, opt => opt.MapFrom(src => src.Notification))
            .ForMember(dest => dest.UserNotificationSettings, opt => opt.MapFrom(src => src.UserNotificationSettings))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Ts, opt => opt.MapFrom(src => src.Ts));
        
        CreateMap<NotificationModel, NotificationDto>();
        
        CreateMap<UserNotificationSettingsModel, UserNotificationSettingsDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Push, opt => opt.MapFrom(src => src.Push));
        
        CreateMap<EmailNotificationSettingsModel, EmailNotificationSettingsDto>();
        
        CreateMap<PushNotificationSettingsModel, PushNotificationSettingsDto>();
    }
}
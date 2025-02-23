using AutoMapper;
using TalentHub.NotificationService.Application.DTO;
using TalentHub.NotificationService.Host.Models;

namespace TalentHub.NotificationService.Host.Mapping;

public class NotificationMappingProfile : Profile
{
    public NotificationMappingProfile()
    {
        CreateMap<NotificationEventModel, NotificationEventDto>()
            .ForMember(dest => dest.Notification, opt => opt.MapFrom(src => src.Notification))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Ts, opt => opt.MapFrom(src => src.Ts));
        
        CreateMap<NotificationModel, NotificationDto>();
    }
}
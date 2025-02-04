using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TalentHub.NotificationService.Host.Mapping;

namespace TalentHub.NotificationService.Host.Extensions;

public static class MapperRegistrarExtension
{
    public static IServiceCollection RegisterMapper(this IServiceCollection services)
    {
        services.AddSingleton<IMapper>(
            new Mapper(
                new MapperConfiguration(cfg => 
                    cfg.AddProfile<NotificationMappingProfile>())));
        
        return services;
    }
}
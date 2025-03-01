using Microsoft.Extensions.DependencyInjection;
using TalentHub.NotificationService.Application.Models;
using TalentHub.NotificationService.Host.Configurations;
using TalentHub.NotificationService.Host.Settings;

namespace TalentHub.NotificationService.Host.Extensions;

public static class OptionsRegistrarExtension
{
    public static IServiceCollection RegisterOptions(this IServiceCollection services)
    {
        services.AddOptions<SeqConfiguration>()
            .BindConfiguration(nameof(SeqConfiguration));
        
        services.AddOptions<SmtpConfiguration>()
            .BindConfiguration(nameof(SmtpConfiguration));
        
        services.AddOptions<FirebaseConfiguration>()
            .BindConfiguration(nameof(FirebaseConfiguration));
        
        services.AddOptions<RabbitMqConfiguration>()
            .BindConfiguration(nameof(RabbitMqConfiguration));
        
        services.AddOptions<UserServiceClientConfiguration>()
            .BindConfiguration(nameof(UserServiceClientConfiguration));
        
        services.AddOptions<HttpClientErrorPolicy>()
            .BindConfiguration(nameof(HttpClientErrorPolicy));
        
        return services;
    }
}
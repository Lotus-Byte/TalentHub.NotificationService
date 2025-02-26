using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using TalentHub.NotificationService.Application.Abstractions;
using TalentHub.NotificationService.Application.Models;
using TalentHub.NotificationService.Application.Services.Clients;

namespace TalentHub.NotificationService.Host.Extensions;

public static class HttpClientRegistrarExtension
{
    public static IServiceCollection RegisterHttpClients(this IServiceCollection services)
    {
        return services
            .RegisterUserServiceClient()
            .RegisterFirebaseServiceClient();
    }

    private static IServiceCollection RegisterUserServiceClient(this IServiceCollection services)
    {
        services.AddHttpClient<IUserServiceClient, UserServiceClient>((context, client) =>
            {
                var configuration = context.GetService<IOptions<UserServiceClientConfiguration>>()
                                    ?? throw new ConfigurationException($"Lack of '{nameof(UserServiceClientConfiguration)}' settings");

                var userServiceClientConfiguration = configuration.Value;
        
                client.BaseAddress = new Uri(userServiceClientConfiguration.Endpoint);
            })
            .AddPolicyHandler((context, _) =>
            {
                var configuration = context.GetService<IOptions<HttpClientErrorPolicy>>()
                                    ?? throw new ConfigurationException($"Lack of '{nameof(HttpClientErrorPolicy)}' settings");
        
                var retryConfiguration = configuration.Value;
        
                return Policy<HttpResponseMessage>
                    .Handle<HttpRequestException>()
                    .OrResult(x => !x.IsSuccessStatusCode)
                    .WaitAndRetryAsync(
                        retryConfiguration.RetryCount,
                        _ => TimeSpan.FromSeconds(retryConfiguration.SleepDuration)
                    );
            });
        
        return services;
    }
    
    private static IServiceCollection RegisterFirebaseServiceClient(this IServiceCollection services)
    {
        services.AddHttpClient<IFirebaseServiceClient, FirebaseServiceClient>((context, client) =>
            {
                var configuration = context.GetService<IOptions<FirebaseConfiguration>>()
                                    ?? throw new ConfigurationException($"Lack of '{nameof(FirebaseConfiguration)}' settings");

                var firebaseConfiguration = configuration.Value;
        
                client.BaseAddress = new Uri(firebaseConfiguration.Url);
            })
            .AddPolicyHandler((context, _) =>
            {
                var configuration = context.GetService<IOptions<HttpClientErrorPolicy>>()
                                    ?? throw new ConfigurationException($"Lack of '{nameof(HttpClientErrorPolicy)}' settings");
        
                var retryConfiguration = configuration.Value;
        
                return Policy<HttpResponseMessage>
                    .Handle<HttpRequestException>()
                    .OrResult(x => !x.IsSuccessStatusCode)
                    .WaitAndRetryAsync(
                        retryConfiguration.RetryCount,
                        _ => TimeSpan.FromSeconds(retryConfiguration.SleepDuration)
                    );
            });
        
        return services;
    }
}
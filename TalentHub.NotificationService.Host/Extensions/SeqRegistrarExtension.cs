using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TalentHub.NotificationService.Host.Configurations;

namespace TalentHub.NotificationService.Host.Extensions;

public static class SeqRegistrarExtension
{
    public static ILoggingBuilder RegisterSeq(this ILoggingBuilder logging)
    {
        var configuration = logging.Services.BuildServiceProvider()
            .GetService<IOptions<SeqConfiguration>>()
                         ?? throw new ConfigurationException($"Lack of '{nameof(SeqConfiguration)}' settings");
        
        var seqConfiguration = configuration.Value;

        if (seqConfiguration == null)
        {
            throw new InvalidOperationException($"Could not find '{nameof(SeqConfiguration)}'");
        }

        return logging
            .ClearProviders()
            .AddSeq(seqConfiguration.ServerUrl);
    }
}
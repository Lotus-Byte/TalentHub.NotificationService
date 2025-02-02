using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TalentHub.NotificationService.Host;

public class MassTransitService : IHostedService
{
    private readonly IBusControl _busControl;
    private readonly ILogger<MassTransitService> _logger;

    public MassTransitService(ILogger<MassTransitService> logger, IBusControl busControl)
    {
        _logger = logger;
        _busControl = busControl;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _busControl.StartAsync(cancellationToken);
        _logger.LogInformation("MassTransit Service is starting.");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _busControl.StopAsync(cancellationToken);
        _logger.LogInformation("MassTransit Service is stopping.");
    }
}
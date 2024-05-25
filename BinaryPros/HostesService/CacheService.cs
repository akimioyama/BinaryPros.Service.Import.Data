using Logic;
using Microsoft.Extensions.Logging;

namespace Service.HostesService;

public sealed class CacheService : BackgroundService
{
    public CacheService(ILogger<CacheService> logger, ICacheProcess cacheProcess)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _cacheProcess = cacheProcess ?? throw new ArgumentNullException(nameof(cacheProcess));
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Start cache initialization");

        await _cacheProcess.InitializationAsync(cancellationToken);

        _logger.LogInformation("Initialization done");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //await _cacheProcess.ReceivingAsync(stoppingToken);
    }

    private readonly ILogger<CacheService> _logger;
    private readonly ICacheProcess _cacheProcess;
}

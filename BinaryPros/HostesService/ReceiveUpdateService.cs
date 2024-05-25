using Logic;

namespace Service.HostesService;

public class ReceiveUpdateService : BackgroundService
{
    public ReceiveUpdateService(ILogger<ReceiveUpdateService> logger, IMainProcess mainProcess)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _mainProcess = mainProcess ?? throw new ArgumentNullException(nameof(mainProcess));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _mainProcess.ProcessAsync(stoppingToken).ConfigureAwait(false);
    }

    private readonly ILogger<ReceiveUpdateService> _logger;
    private readonly IMainProcess _mainProcess;
}

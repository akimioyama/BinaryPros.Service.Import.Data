using Logic.Abstractions.Handle;
using Logic.Abstractions.Transport;
using Logic.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Logic;

public sealed class MainProcess : IMainProcess
{
    public MainProcess(
        ILogger<MainProcess> logger, 
        IRequestSender request, 
        IMainHandle handle,
        IOptions<MainConfiguration> options)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _requestSender = request ?? throw new ArgumentNullException(nameof(request));

        _mainHandle = handle ?? throw new ArgumentNullException(nameof(handle));

        ArgumentNullException.ThrowIfNull(options);
        _configuration = options.Value;
    }

    public async Task ProcessAsync(CancellationToken token)
    {
        while (true)
        {
            token.ThrowIfCancellationRequested();

            _logger.LogInformation("Sned request");

            var lsit = await _requestSender.SendAsync(token);

            _logger.LogInformation("Start main {Handle}", nameof(_mainHandle));

            await _mainHandle.HandleAsync(lsit, token);

            _logger.LogInformation("Process done. Start deley");

            await Task.Delay(_configuration.DelayBetweenRequests, token);
        }
    }

    private readonly ILogger<MainProcess> _logger;
    private readonly IRequestSender _requestSender;
    private readonly IMainHandle _mainHandle;
    private readonly MainConfiguration _configuration;
}

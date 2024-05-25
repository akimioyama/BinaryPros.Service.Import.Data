using Logic.Abstractions.Transport;
using Logic.Abstractions.Transport.Deserializer;
using Logic.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models.Entity;
using System.Collections.Generic;

namespace Logic.Transport;

public sealed class RequestSender : IRequestSender
{
    public RequestSender(
        ILogger<RequestSender> logger,
        IDeserializer deserializer,
        IOptions<RequestConfiguration> options)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));

        ArgumentNullException.ThrowIfNull(options);
        _configuration = options.Value;
    }

    public async Task<IReadOnlyCollection<FeedEntity>> SendAsync(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        try
        {
            var response = await _httpClient.GetAsync(_configuration.EndPoint);

            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadAsStringAsync();

                _logger.LogDebug("Response: '{Response}'", response);

                _logger.LogDebug("Conent: '{Conent}'", list);

                return _deserializer.Deserialization(list);
            }
            else
            {
                return [];
            }
        }
        catch (Exception ex)
        {
            _logger.LogDebug("Exception: '{Exception}'", ex.Message);

            return [];
        }
    }

    private readonly ILogger<RequestSender> _logger;
    private readonly IDeserializer _deserializer;
    private readonly RequestConfiguration _configuration;
    private static HttpClient _httpClient = new();
}

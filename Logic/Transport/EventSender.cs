using Confluent.Kafka;
using Logic.Abstractions.Transport;
using Logic.Abstractions.Transport.Convertor;
using Logic.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models.Common.DomainEvents;

namespace Logic.Transport;

public sealed class EventSender : IEventSender
{
    public EventSender(
        ILogger<EventSender> logger,
        IConverter<IDomainEvent, byte[]> converter,
        IOptions<KafkaConfiguration> options)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _converter = converter ?? throw new ArgumentNullException(nameof(converter));

        ArgumentNullException.ThrowIfNull(options);
        _configuration = options.Value;
    }

    public async Task EventSendAsync(IDomainEvent domainEvent, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        _logger.LogInformation("Send entity");

        await SendEvent(domainEvent, token);
    }

    private async Task SendEvent(IDomainEvent domainEvent, CancellationToken token)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = _configuration.BootstrapServers
        };

        using var producer = new ProducerBuilder<string, byte[]>(config).Build();

        var message = _converter.Convert(domainEvent);

        var status = await producer.ProduceAsync(
            _configuration.TopicTo, 
            new Message<string, byte[]> 
            { 
                Key = domainEvent.EntityId.Value, 
                Value = message 
            },
            token);

        if (status.Status == PersistenceStatus.Persisted)
        {
            _logger.LogDebug("Message successfully sent");
        }
        else
        {
            _logger.LogDebug("Error when sending a message");
        }
    }

    private readonly ILogger<EventSender> _logger;
    private readonly IConverter<IDomainEvent, byte[]> _converter;
    private readonly KafkaConfiguration _configuration;
}

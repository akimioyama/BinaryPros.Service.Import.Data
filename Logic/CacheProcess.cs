using Confluent.Kafka;
using Logic.Abstractions.Cache;
using Logic.Abstractions.Transport.Deserializer;
using Logic.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models.Common;
using Models.Entity;

namespace Logic;

public sealed class CacheProcess : ICacheProcess
{
    public CacheProcess(
        ILogger<CacheProcess> logger,
        IEntityCache<Identifier<FeedEntity>, FeedEntity> entityCache,
        IDeserializer deserializer,
        IOptions<KafkaConfiguration> options)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _entityCache = entityCache ?? throw new ArgumentNullException(nameof(entityCache));

        _deserializer = deserializer ?? throw new ArgumentNullException(nameof(deserializer));

        ArgumentNullException.ThrowIfNull(options);
        _configuration = options.Value;
    }

    public async Task InitializationAsync(CancellationToken token)
    {
        _logger.LogDebug("Start Initialization");

        var list = await ConsumeFromKafkaAsync(token);

        var init = new Dictionary<Identifier<FeedEntity>, FeedEntity>();

        foreach (var item in list)
        {
            var entity = _deserializer.DeserializeEntity(item);

            init.Add(entity.Id, entity);
        }

        _entityCache.Init(init);

        _logger.LogDebug("Initialization done");
    }

    public Task<FeedEntity> ReceivingAsync(CancellationToken token)
    {
        throw new NotImplementedException();
    }

    private Task<List<string>> ConsumeFromKafkaAsync(CancellationToken token)
    {
        var result = new List<string>();

        var config = new ConsumerConfig
        {
            BootstrapServers = _configuration.BootstrapServers,
            GroupId = Guid.NewGuid().ToString(),
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<string, string>(config).Build();

        consumer.Subscribe(_configuration.TopicFrom);

        try
        {
            while (true)
            {
                try
                {
                    var consumeResult = consumer.Consume(TimeSpan.FromSeconds(4));

                    if (consumeResult is null)
                    {
                        break;
                    }

                    result.Add(consumeResult.Message.Value);
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occurred: {e.Error.Reason}");
                }
            }
        }
        catch (OperationCanceledException)
        {
            consumer.Close();
        }

        return Task.FromResult(result);
    }

    //private Task<string> ConsumeNewFromKafkaAsync(CancellationToken token)
    //{
    //    var config = new ConsumerConfig
    //    {
    //        BootstrapServers = _configuration.BootstrapServers,
    //        GroupId = "my-consumer-group",
    //        AutoOffsetReset = AutoOffsetReset.Earliest
    //    };

    //    using var consumer = new ConsumerBuilder<string, string>(config).Build();

    //    consumer.Subscribe(_configuration.TopicFrom);

    //    try
    //    {
    //        while (true)
    //        {
    //            try
    //            {
    //                var result = consumer.Consume(token);
    //            }
    //            catch (ConsumeException e)
    //            {
    //                Console.WriteLine($"Error occurred: {e.Error.Reason}");
    //            }
    //        }
    //    }
    //    catch (OperationCanceledException)
    //    {
    //        // Ctrl+C was pressed.
    //    }
    //    finally
    //    {
    //        consumer.Close();
    //    }
    //}

    private readonly ILogger<CacheProcess> _logger;
    private readonly IEntityCache<Identifier<FeedEntity>, FeedEntity> _entityCache;
    private readonly IDeserializer _deserializer;
    private readonly KafkaConfiguration _configuration;
}

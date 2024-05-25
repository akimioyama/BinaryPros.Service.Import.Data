using Logic.Abstractions.Cache;
using Logic.Abstractions.Handle;
using Logic.Abstractions.Transport;
using Microsoft.Extensions.Logging;
using Models.Common;
using Models.Common.DomainEvents;
using Models.Entity;

namespace Logic.Handle;

public sealed class MainHandle : IMainHandle
{
    public MainHandle(
        ILogger<MainHandle> logger,
        IEntityCache<Identifier<FeedEntity>, FeedEntity> entityCache,
        IEventSender eventSender)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _entityCache = entityCache ?? throw new ArgumentNullException(nameof(entityCache));

        _eventSender = eventSender ?? throw new ArgumentNullException(nameof(eventSender));
    }

    public async Task HandleAsync(IReadOnlyCollection<FeedEntity> entities, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        ArgumentNullException.ThrowIfNull(entities);

        foreach (var entity in entities)
        {
            await UpdateCheckerAsync(entity, token);
        }
    }

    private async Task UpdateCheckerAsync(FeedEntity feedEntity, CancellationToken token)
    {
        if (feedEntity.IsCanceled)
        {
            _logger.LogDebug("Feed entity is canceled. Send event.");

            await _eventSender.EventSendAsync(
                new Cancellation(feedEntity.Id, feedEntity.Reason!.Value),
                token);

            return;
        }

        if (!_entityCache.TryGetById(feedEntity.Id, out var cacheEntity))
        {
            _logger.LogDebug("Feed entity not constent in cache. Add them.");

            _entityCache.AddOrUpdate(feedEntity.Id, feedEntity);

            await _eventSender.EventSendAsync(
                new ValueChange(feedEntity.Id, feedEntity.OldValue, feedEntity.NewValue),
                token);

            return;
        }

        _logger.LogDebug("Feed entity in cache and updated.");

        await _eventSender.EventSendAsync(
            new ValueChange(feedEntity.Id, feedEntity.OldValue, feedEntity.NewValue),
            token);
    }

    private readonly ILogger<MainHandle> _logger;
    private readonly IEventSender _eventSender;
    private readonly IEntityCache<Identifier<FeedEntity>, FeedEntity> _entityCache;
}

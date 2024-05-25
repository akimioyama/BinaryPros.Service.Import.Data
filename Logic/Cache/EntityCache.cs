using Logic.Abstractions.Cache;
using Models.Common;
using Models.Entity;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace Logic.Cache;

public sealed class EntityCache : IEntityCache<Identifier<FeedEntity>, FeedEntity>
{
    public void AddOrUpdate(Identifier<FeedEntity> id, FeedEntity entity)
    {
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(entity);

        _cache[id] = entity;
    }

    public void Init(IReadOnlyDictionary<Identifier<FeedEntity>, FeedEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        _cache = new(entities);
    }

    public bool TryGetById(Identifier<FeedEntity> id, [MaybeNullWhen(false)] out FeedEntity entity)
    {
        ArgumentNullException.ThrowIfNull(id);

        return _cache!.TryGetValue(id, out entity);
    }

    private ConcurrentDictionary<Identifier<FeedEntity>, FeedEntity> _cache;
}

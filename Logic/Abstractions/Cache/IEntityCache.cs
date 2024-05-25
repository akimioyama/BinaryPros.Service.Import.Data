using Models.Entity;
using System.Diagnostics.CodeAnalysis;

namespace Logic.Abstractions.Cache;

public interface IEntityCache<TId, TEntity>
    where TId : notnull
    where TEntity : FeedEntity
{
    public void Init(IReadOnlyDictionary<TId, TEntity> entities);

    public bool TryGetById(TId id, [MaybeNullWhen(false)] out TEntity entity);

    public void AddOrUpdate(TId id, TEntity entity);
}

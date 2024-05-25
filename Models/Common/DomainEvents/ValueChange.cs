using Models.Entity;

namespace Models.Common.DomainEvents;

public sealed class ValueChange : IDomainEvent
{
    public Identifier<FeedEntity> EntityId { get; }

    public Importance<FeedEntity> OldValue { get; }

    public Importance<FeedEntity> NewValue { get; }

    public ValueChange(
        Identifier<FeedEntity> entityId, 
        Importance<FeedEntity> oldValue,
        Importance<FeedEntity> newValue)
    {
        EntityId = entityId 
            ?? throw new ArgumentNullException(nameof(entityId));

        OldValue = oldValue 
            ?? throw new ArgumentNullException(nameof(oldValue));

        NewValue = newValue 
            ?? throw new ArgumentNullException(nameof(newValue));
    }
}

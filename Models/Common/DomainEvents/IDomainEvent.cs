using Models.Entity;

namespace Models.Common.DomainEvents;

public interface IDomainEvent
{
    public Identifier<FeedEntity> EntityId { get; }
}

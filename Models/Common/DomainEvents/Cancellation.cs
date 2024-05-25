using Models.Entity;
using System.ComponentModel;

namespace Models.Common.DomainEvents;

public sealed class Cancellation : IDomainEvent
{
    public Identifier<FeedEntity> EntityId { get; }

    public ReasonCancellation Reason {  get; }

    public Cancellation(Identifier<FeedEntity> entityId, ReasonCancellation reason)
    {
        EntityId = entityId 
            ?? throw new ArgumentNullException(nameof(entityId));

        if (Enum.IsDefined(typeof(ReasonCancellation), reason))
        {
            throw new InvalidEnumArgumentException(
                nameof(reason),
                (int)reason,
                typeof(ReasonCancellation));
        }

        Reason = reason;
    }
}

using Models.Common;
using System.ComponentModel;

namespace Models.Entity;

public class FeedEntity : IEntity
{
    public Identifier<FeedEntity> Id { get; }

    public EntityName<FeedEntity> Name { get; }

    public Importance<FeedEntity> NewValue { get; }

    public Importance<FeedEntity> OldValue { get; }

    public DateTimeOffset Timespan { get; }

    public EntityType EntityType { get; }

    public ChangeType ChangeType { get; }

    public Source Source { get; }

    public bool IsCanceled { get; }

    public ReasonCancellation? Reason { get; }

    public FeedEntity(
        Identifier<FeedEntity> id, 
        EntityName<FeedEntity> name, 
        Importance<FeedEntity> newValue, 
        Importance<FeedEntity> oldValue, 
        DateTimeOffset timespan, 
        EntityType entityType, 
        ChangeType changeType, 
        Source source,
        bool isCanceled,
        ReasonCancellation? reason)
    {
        Id = id
            ?? throw new ArgumentNullException(nameof(id));

        Name = name 
            ?? throw new ArgumentNullException(nameof(name));

        NewValue = newValue 
            ?? throw new ArgumentNullException(nameof(newValue));

        OldValue = oldValue 
            ?? throw new ArgumentNullException(nameof(oldValue));

        Timespan = timespan;

        if (!Enum.IsDefined(typeof(EntityType), entityType))
        {
            throw new InvalidEnumArgumentException(
                nameof(entityType), 
                (int)entityType, 
                typeof(EntityType));
        }

        EntityType = entityType;

        ChangeType = changeType;

        if (!Enum.IsDefined(typeof(ChangeType), changeType))
        {
            throw new InvalidEnumArgumentException(
                nameof(changeType),
                (int)changeType,
                typeof(ChangeType));
        }

        Source = source;

        if (!Enum.IsDefined(typeof(Source), source))
        {
            throw new InvalidEnumArgumentException(
                nameof(source),
                (int)source,
                typeof(Source));
        }

        IsCanceled = IsCanceled;
        Reason = reason;
    }
}

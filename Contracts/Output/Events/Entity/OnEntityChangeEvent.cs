using MessagePack;

namespace Contracts.Output.Events.Entity;

[MessagePackObject]
public sealed class OnEntityChangeEvent : EntityRelatedEvent
{
    [Key(1)]
    public required decimal OldValue { get; init; }

    [Key(2)]
    public required decimal NewValue { get; init; }
}

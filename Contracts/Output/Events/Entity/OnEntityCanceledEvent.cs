using MessagePack;

namespace Contracts.Output.Events.Entity;

[MessagePackObject]
public sealed class OnEntityCanceledEvent : EntityRelatedEvent
{
    [Key(1)]
    public required string Reason { get; init; }
}

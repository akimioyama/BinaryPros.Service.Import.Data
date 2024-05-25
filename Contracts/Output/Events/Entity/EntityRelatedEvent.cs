using MessagePack;

namespace Contracts.Output.Events.Entity;

[MessagePackObject]
[Union(0, typeof(OnEntityChangeEvent))]
[Union(1, typeof(OnEntityCanceledEvent))]
public abstract class EntityRelatedEvent : BaseFixtureChangedEvent
{
}

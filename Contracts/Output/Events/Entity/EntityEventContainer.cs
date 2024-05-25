using MessagePack;

namespace Contracts.Output.Events.Entity;

[MessagePackObject]
public sealed class EntityEventContainer :
    FixtureChangedEventContainerBase<EntityRelatedEvent>
{ }

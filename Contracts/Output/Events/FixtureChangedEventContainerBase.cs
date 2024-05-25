using MessagePack;

namespace Contracts.Output.Events;

public abstract class FixtureChangedEventContainerBase<TEventBase> : IFixtureChangedEventContainer
    where TEventBase : BaseFixtureChangedEvent
{
    [IgnoreMember]
    public BaseFixtureChangedEvent BaseFixtureChangedEvent => InnerFixtureChangedEvent;

    [Key(0)]
    public required string MessageId { get; init; }

    [Key(1)]
    public required TEventBase InnerFixtureChangedEvent { get; init; }
}

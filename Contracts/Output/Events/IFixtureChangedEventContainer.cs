using Contracts.Output.Events.Entity;
using MessagePack;

namespace Contracts.Output.Events;

[Union(0, typeof(EntityEventContainer))]
public interface IFixtureChangedEventContainer
{
    public BaseFixtureChangedEvent BaseFixtureChangedEvent { get; }

    public string MessageId { get; }
}

using MessagePack;

namespace Contracts.Output.Events;

public abstract class BaseFixtureChangedEvent
{
    [Key(0)]
    public required string Identifier { get; init; }
}

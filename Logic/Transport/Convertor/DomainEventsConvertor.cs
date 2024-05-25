using Contracts.Output.Events;
using Contracts.Output.Events.Entity;
using Logic.Abstractions.Transport.Convertor;
using MessagePack;
using Models.Common.DomainEvents;

namespace Logic.Transport.Convertor;

public sealed class DomainEventsConvertor : IConverter<IDomainEvent, byte[]>
{
    public byte[] Convert(IDomainEvent source)
    {
        ArgumentNullException.ThrowIfNull(source);

        var messageContainer = GetMessageContainer(source, Guid.NewGuid().ToString());

        return MessagePackSerializer.Serialize(messageContainer, _options);
    }

    private static IFixtureChangedEventContainer GetMessageContainer(
        IDomainEvent domain,
        string messageId) =>
        domain switch
        {
            Cancellation cancellation =>
                new EntityEventContainer
                {
                    MessageId = messageId,
                    InnerFixtureChangedEvent = new OnEntityCanceledEvent
                    {
                        Identifier = cancellation.EntityId.Value,
                        Reason = cancellation.Reason.ToString()
                    }
                },

            ValueChange change =>
                new EntityEventContainer
                {
                    MessageId = messageId,
                    InnerFixtureChangedEvent = new OnEntityChangeEvent
                    {
                        Identifier = change.EntityId.Value,
                        OldValue = change.OldValue.Value,
                        NewValue = change.NewValue.Value
                    }
                },

            _ => throw new InvalidOperationException(
                $"The event type {domain.GetType().Name} is not supported.")
        };

    private readonly MessagePackSerializerOptions _options =
        MessagePackSerializerOptions.Standard
            .WithCompression(MessagePackCompression.Lz4Block);
}

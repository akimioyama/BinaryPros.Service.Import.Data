using Models.Common.DomainEvents;

namespace Logic.Abstractions.Transport;

public interface IEventSender
{
    public Task EventSendAsync(IDomainEvent domainEvent, CancellationToken token);
}

using Models.Entity;

namespace Logic.Abstractions.Transport;

public interface IRequestSender
{
    Task<IReadOnlyCollection<FeedEntity>> SendAsync(CancellationToken token);
}

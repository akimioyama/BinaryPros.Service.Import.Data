using Models.Entity;

namespace Logic.Abstractions.Handle;

public interface IMainHandle
{
    Task HandleAsync(IReadOnlyCollection<FeedEntity> entities, CancellationToken token);
}

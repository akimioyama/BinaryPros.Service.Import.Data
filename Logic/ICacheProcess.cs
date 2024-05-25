using Models.Entity;

namespace Logic;

public interface ICacheProcess
{
    public Task InitializationAsync(CancellationToken token);

    public Task<FeedEntity> ReceivingAsync(CancellationToken token);
}

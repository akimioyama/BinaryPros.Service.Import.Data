namespace Logic;

public interface IMainProcess
{
    Task ProcessAsync(CancellationToken token);
}

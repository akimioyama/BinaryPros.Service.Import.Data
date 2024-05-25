namespace Logic.Configuration;

public sealed class KafkaConfiguration
{
    public string BootstrapServers { get; init; }

    public string TopicFrom { get; init; }

    public string TopicTo { get; init; }
}

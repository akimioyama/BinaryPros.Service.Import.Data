using Contracts.Input;
using Logic.Abstractions.Transport.Deserializer;
using Models.Entity;
using Newtonsoft.Json;

namespace Logic.Transport.Deserializer;

public sealed class Deserializer : IDeserializer
{
    public IReadOnlyCollection<FeedEntity> Deserialization(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                "Source cannot be empty, or null, or has only white-space characters.",
                nameof(value));
        }

        var entities = JsonConvert.DeserializeObject<List<FeedEntityTransport>>(value);

        var result = new List<FeedEntity>();

        foreach (var item in entities)
        {
            result.Add(
                new FeedEntity(
                    new(item.Id.Value),
                    new(item.Name.Value),
                    new(item.NewValue.Value),
                    new(item.OldValue.Value),
                    item.Timespan,
                    item.EntityType,
                    item.ChangeType,
                    item.Source,
                    item.IsCanceled,
                    item.Reason));
        }

        return result;
    }

    public FeedEntity DeserializeEntity(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(
                "Source cannot be empty, or null, or has only white-space characters.",
                nameof(value));
        }

        var item = JsonConvert.DeserializeObject<FeedEntityTransport>(value)!;

        return new FeedEntity(
                new(item.Id.Value),
                new(item.Name.Value),
                new(item.NewValue.Value),
                new(item.OldValue.Value),
                item.Timespan,
                item.EntityType,
                item.ChangeType,
                item.Source,
                item.IsCanceled,
                item.Reason);
    }
}

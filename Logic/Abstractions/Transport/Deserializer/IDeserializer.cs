using Models.Entity;

namespace Logic.Abstractions.Transport.Deserializer;

public interface IDeserializer
{
    public IReadOnlyCollection<FeedEntity> Deserialization(string value);

    public FeedEntity DeserializeEntity(string value);
}

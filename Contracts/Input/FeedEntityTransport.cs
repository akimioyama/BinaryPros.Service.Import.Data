using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Contracts.Input;

public sealed class FeedEntityTransport
{
    [JsonProperty(PropertyName = "id", Required = Required.Always)]
    public required Id Id { get; init; }

    [JsonProperty(PropertyName = "name", Required = Required.Always)]
    public required Name Name { get; init; }

    [JsonProperty(PropertyName = "new_value", Required = Required.Always)]
    public required ValueQ NewValue { get; init; }

    [JsonProperty(PropertyName = "old_value", Required = Required.Always)]
    public required ValueQ OldValue { get; init; }

    [JsonProperty(PropertyName = "timespan", Required = Required.Always)]
    public required DateTimeOffset Timespan { get; init; }

    [JsonProperty(PropertyName = "entity_type", Required = Required.Always)]
    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    public required EntityType EntityType { get; init; }

    [JsonProperty(PropertyName = "change_type", Required = Required.Always)]
    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    public required ChangeType ChangeType { get; init; }

    [JsonProperty(PropertyName = "source", Required = Required.Always)]
    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    public required Source Source { get; init; }

    [JsonProperty(PropertyName = "is_canceled", Required = Required.Always)]
    public bool IsCanceled {  get; init; }

    [JsonProperty(PropertyName = "reason_cancellation", Required = Required.AllowNull)]
    [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
    public required ReasonCancellation? Reason { get; init; }
}

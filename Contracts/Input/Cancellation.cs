using Models;
using Newtonsoft.Json;

namespace Contracts.Input;

public sealed class Cancellation
{
    [JsonProperty(PropertyName = "id", Required = Required.Always)]
    public required string Id { get; init; }

    [JsonProperty(PropertyName = "is_canceled", Required = Required.Always)]
    public required bool IsCanceled { get; init; }

    [JsonProperty(PropertyName = "reason", Required = Required.Always)]
    public required ReasonCancellation Reason { get; init; }
}

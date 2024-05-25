using Newtonsoft.Json;

namespace Contracts.Input;

public sealed class ValueQ
{
    [JsonProperty(PropertyName = "value", Required = Required.Always)]
    public decimal Value { get; init; }
}

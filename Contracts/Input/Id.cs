using Newtonsoft.Json;

namespace Contracts.Input;

public sealed class Id
{
    [JsonProperty(PropertyName = "value", Required = Required.Always)]
    public string Value { get; init; }
}

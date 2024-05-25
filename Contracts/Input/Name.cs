using Newtonsoft.Json;

namespace Contracts.Input;

public sealed class Name
{
    [JsonProperty(PropertyName = "value", Required = Required.Always)]
    public string Value { get; init; }
}
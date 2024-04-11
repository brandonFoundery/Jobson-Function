using System.Text.Json.Serialization;

namespace Shared;

public class StreamMessage
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("role")]
    public string Role { get; set; }

    [JsonPropertyName("content")]
    public List<object> Content { get; set; }

    [JsonPropertyName("model")]
    public string Model { get; set; }

    [JsonPropertyName("stop_reason")]
    public object StopReason { get; set; }

    [JsonPropertyName("stop_sequence")]
    public object StopSequence { get; set; }

    [JsonPropertyName("usage")]
    public Usage Usage { get; set; }
}
using System.Text.Json.Serialization;

namespace Shared;

public class Delta
{
    [JsonPropertyName("stop_reason")]
    public string StopReason { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }
    [JsonPropertyName("usage")]
    public Usage Usage { get; set; }
}
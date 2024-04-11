using System.Text.Json.Serialization;

namespace Shared;

public class Content
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}
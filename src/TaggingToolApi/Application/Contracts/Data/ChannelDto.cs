using System.Text.Json.Serialization;

namespace Application.Contracts.Data;

public class ChannelDto
{
    
    [JsonPropertyName("launchDate")]
    public string LaunchDate { get; init; } = default!;
    
    [JsonPropertyName("title")]
    public string Title { get; init; } = default!;
    
    [JsonPropertyName("channelTags")]
    public IEnumerable<string> Tags { get; init; } = default!;
    
}